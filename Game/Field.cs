namespace Game
{
    public class Field
    {
        private readonly List<Ship> _ships;
        public List<Ship> Ships { get { return _ships.Copy(); } }

        private static readonly Array _marks = Enum.GetValues(typeof(FieldMarks));

        private readonly FieldMarks[,] _field;

        private static readonly Random _random = new();

        static Field()
        {
            if (!LettersCount.InRange(1, AlphabetSize))
                throw new ArgumentException($"settings argument should be in the range from 1 to {AlphabetSize}", nameof(LettersCount));

            if (!NumbersCount.InRange(1, NumbersSize))
                throw new ArgumentException($"settings argument should be in range from 1 to {NumbersSize}", nameof(NumbersCount));
        }

        public Field()
        {
            _field = new FieldMarks[LettersCount, NumbersCount];
            _ships = new List<Ship>();
        }

        public FieldMarks this[int index1, int index2] 
        { 
            get { return _field[index1, index2]; } 
            private set { _field[index1, index2] = value; }
        }

        public FieldMarks this[FieldCoords coords]
        {
            get { return _field[coords.X, coords.Y]; }
            private set { _field[coords.X, coords.Y] = value; }
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
            if (GetShipByCoords(coords, out Ship? ship))
            {
                ship!.Hit(coords);

                if (ship.IsDestroyed())
                    SetDestroyFrame(ship.GetDestroyFrame());

                this[coords] = FieldMarks.Hit;
                isNeedSwitching = false;
            }
            else
            {
                this[coords] = FieldMarks.Miss;
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

            for (int y = 1; y < fieldAsLines.Length; y++)
            {
                var line = fieldAsLines[y]; 

                for (int x = 1; x < line.Length; x++)
                {
                    var character = line[x];
                    this[x - 1, y - 1] = (FieldMarks)character;
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

        public FieldCoords GetRandomFreeCoords(int startX = 0, int endX = LettersCount, 
                                               int startY = 0, int endY = NumbersCount)
        {
            var x = _random.Next(startX, endX);
            var y = _random.Next(startY, endY);

            return GetRegionalFreeCoords(x, x, y, y);
        }

        public FieldCoords GetRegionalFreeCoords(int startX, int endX, 
                                                 int startY, int endY)
        {
            var freeCoords = GenerateFreeCoordsList(startX, endX, startY, endY);

            return freeCoords[_random.Next(freeCoords.Count)];
        }

        private List<FieldCoords> GenerateFreeCoordsList(int startX, int endX,
                                                         int startY, int endY)
        {
            var regionalFreeCoords = new List<FieldCoords>();

            for (int tX = startX - 1; tX <= endX + 1; tX++)
                for (int tY = startY - 1; tY <= endY + 1; tY++)
                    if (IsInFieldRange(tX, tY)
                     && IsFree(tX, tY)
                     && !IsAreaCorner(tX, tY, startX, endX, startY, endY))
                        regionalFreeCoords.Add(new FieldCoords(tX, tY));

            return regionalFreeCoords;
        }

        private static bool IsAreaCorner(int x, int y, 
                                         int startX, int endX, 
                                         int startY, int endY)
        {
            return (x, y) == (startX - 1, startY - 1)
                || (x, y) == (startX - 1, endY + 1)
                || (x, y) == (endX + 1, endY + 1)
                || (x, y) == (endX + 1, endY - 1);
        }

        private bool IsFree(int x, int y)
        {
            return !(IsHit(x, y) || IsMiss(x, y));
        }

        public bool GetShipByCoords(FieldCoords coords, out Ship? ship)
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

        public bool IsHit(FieldCoords coords)
        {
            return this[coords] is FieldMarks.Hit;
        }

        public bool IsHit(int x, int y)
        {
            return this[x, y] is FieldMarks.Hit;
        }

        public bool IsMiss(FieldCoords coords)
        {
            return this[coords] is FieldMarks.Miss;
        }

        public bool IsMiss(int x, int y)
        {
            return this[x, y] is FieldMarks.Miss;
        }

        private static bool IsEmpty(char character)
        {
            return character is (char)FieldMarks.Empty;
        }

        private static bool IsShip(char character)
        {
            return character is (char)FieldMarks.Ship;
        }

        private bool IsShip(FieldCoords coords)
        {
            return this[coords] is FieldMarks.Ship;
        }

        private void SetDestroyFrame(List<FieldCoords> frame)
        {
            foreach (var coords in frame)
                this[coords] = FieldMarks.Miss;
        }

        private static bool IsFieldInputFileCorrupted(string[] fieldAsLines)
        {
            for (int i = 0; i < fieldAsLines.Length; i++)
                if (fieldAsLines[i].Length != LettersCount + 1)
                    return true;

            for (int i = 1; i < fieldAsLines.Length; i++)
            {
                var line = fieldAsLines[i];

                for (int j = 1; j < line.Length; j++)
                {
                    var character = line[j];

                    if (!IsCharacterFieldMark(character)
                     || !IsEmpty(character)
                     && !IsShip(character))
                        return true;
                }
            }

            return fieldAsLines.Length != NumbersCount + 1;
        }

        private bool IsShipDefinedOnField(FieldCoords coords)
        {
            foreach (var ship in _ships)
                if (ship.Belongs(coords))
                    return true;

            return false;
        }

        private void ParseShips()
        {
            for (int x = 0; x < _field.GetLength(0); x++)
            {
                for (int y = 0; y < _field.GetLength(1); y++)
                {
                    var coords = new FieldCoords(x, y);

                    if (IsShip(coords)
                    && !IsShipDefinedOnField(coords))
                        _ships.Add(new Ship(coords, _field));
                }
            }
        }
    }
}