namespace Game
{
    public class Field
    {
        private readonly List<Ship> _ships;
        public List<Ship> Ships { get { return _ships.Copy(); } }

        private static readonly Array _marks = Enum.GetValues(typeof(FieldMarks));

        private readonly FieldMarks[,] _field;

        static Field()
        {
            if (!LettersCount.InRange(1, AlphabetSize))
                throw new ArgumentException($"settings argument should be in the range from 1 to 25", nameof(LettersCount));

            if (!NumbersCount.InRange(1, NumbersSize))
                throw new ArgumentException($"settings argument should be more than 0", nameof(NumbersCount));
        }

        public Field()
        {
            _field = new FieldMarks[LettersCount, NumbersCount];
            _ships = new List<Ship>();
        }

        public FieldMarks this[int index1, int index2] 
        { 
            get 
            { 
                return _field[index1, index2]; 
            } 
            private set
            {
                _field[index1, index2] = value;
            }
        }

        public static bool IsInFieldRange(int x, int y)
        {
            return FieldCoords.IsValidCoordX(x)
                && FieldCoords.IsValidCoordY(y);
        }

        public static bool IsCharacterFieldMark(char character)
        {
            foreach (var value in _marks)
                if (character == (char)(FieldMarks)value)
                    return true;

            return false;
        }

        public void ProduceAttack(FieldCoords coords, out bool isNeedSwitching)
        {
            if (this[coords.Y, coords.X] is FieldMarks.Ship)
            {
                var shipExists = GetShipByCoords(coords.Reverse(), out Ship? ship);

                if (shipExists)
                {
                    ship.Hit();

                    if (ship.IsDestroyed())
                        SetDestroyFrame(ship.GetDestroyFrame());
                }

                this[coords.Y, coords.X] = FieldMarks.Hit;
                isNeedSwitching = false;
            }
            else
            {

                this[coords.Y, coords.X] = FieldMarks.Miss;
                isNeedSwitching = true;
            }
        }

        public void ParseFieldFromFile(FileInfo file)
        {
            if (_ships.Count != 0)
                _ships.Clear();
                    
            var fieldAsLines = File.ReadAllLines(file.FullName);

            if (IsFieldInputFileCorrupted(fieldAsLines))
                throw new FileLoadException("Input file is corrupted", nameof(file.FullName));

            for (int i = 1; i < fieldAsLines.Length; i++)
            {
                var line = fieldAsLines[i];
                for (int j = 1; j < line.Length; j++)
                {
                    var character = line[j];
                    if (IsCharacterFieldMark(character))
                    {
                        if (character is not (char)FieldMarks.Ship
                         && character is not (char)FieldMarks.Empty)
                            throw new FileLoadException("Input file is corrupted", nameof(file.FullName));

                        (var cX, var cY) = (i - 1, j - 1);

                        _field[cX, cY] = (FieldMarks)character;
                    }
                    else
                        throw new FileLoadException($"Invalid mark \"{character}\"");
                }
            }

            ParseShips();
        }

        public int GetLength(int dimension)
        {
            return _field.GetLength(dimension);
        }

        public bool IsAllShipsDestroyed()
        {
            var destroyings = 0;

            foreach (var ship in _ships)
                if (ship.IsDestroyed())
                    destroyings++;

            return destroyings == _ships.Count;
        }

        private void SetDestroyFrame(List<FieldCoords> frame)
        {
            foreach (var coords in frame)
                _field[coords.X, coords.Y] = FieldMarks.Miss;
        }

        private static bool IsFieldInputFileCorrupted(string[] fieldAsLines)
        {
            for (int i = 0; i < fieldAsLines.Length; i++)
                if (fieldAsLines[i].Length != LettersCount + 1)
                    return true;

            return fieldAsLines.Length != NumbersCount + 1;
        }

        private bool GetShipByCoords(FieldCoords coords, out Ship? ship)
        {
            foreach (var _ship in _ships)
                if (_ship.Belongs(coords))
                {
                    ship = _ship;
                    return true;
                }

            ship = null;
            return false;
        }

        private bool IsShipDefinedOnField(FieldCoords coords)
        {
            foreach (var ship in _ships)
            {
                if (ship.Belongs(coords))
                    return true;
            }

            return false;
        }

        private void ParseShips()
        {
            for (int x = 0; x < _field.GetLength(0); x++)
            {
                for (int y = 0; y < _field.GetLength(1); y++)
                {
                    var coords = new FieldCoords(x, y);

                    if (_field[x, y] is FieldMarks.Ship
                    && !IsShipDefinedOnField(coords))
                        _ships.Add(new Ship(coords, _field));
                }
            }
        }
    }
}