using System.Drawing;

namespace SeaBattle
{
    internal class Ship
    {
        private readonly List<FieldCoords> _location;
        public List<FieldCoords> Location { get { return _location.Copy(); } }

        private int _hits;
        public int Hits 
        { 
            get 
            { 
                return _hits; 
            }
            set
            {
                _hits = value;
            }
        }

        public int Size { get { return _location.Count; } }

        public Ship(FieldCoords coords, FieldMarks[,] searchField)
        {
            _location = new List<FieldCoords>();
            Reproduce(coords, searchField);
        }

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = 0; i < _location.Count - 1; i++)
                result += $"{_location[i]}, ";

            result += _location[^1];                

            return $"[ {result} ]";
        }

        public bool IsDestroyed()
        {
            return Size == _hits;
        }

        public bool Belongs(FieldCoords coords)
        {
            foreach (var _coords in _location)
                if (_coords.X == coords.X 
                 && _coords.Y == coords.Y)
                    return true;

            return false;
        }

        public List<FieldCoords> GetDestroyFrame()
        {
            var result = new List<FieldCoords>();

            foreach (var elem in _location)
            {//
                if (Field.IsInFieldRange(elem.X + 1, elem.Y) && !Belongs(new FieldCoords(elem.X + 1, elem.X + 1)))
                    result.Add(elem);
                if (Field.IsInFieldRange(elem.X, elem.Y + 1) && !Belongs(new FieldCoords(elem.X, elem.Y + 1)))
                    result.Add(elem);
            }

            return result;
        }

        private void Reproduce(FieldCoords coords, FieldMarks[,] searchField)
        {
            if (Belongs(coords))
                return;

            _location.Add(coords);

            for (int x = coords.X - 1; x <= coords.X + 1; x++)
            {
                for (int y = coords.Y - 1; y <= coords.Y + 1; y++)
                {
                    if (Field.IsInFieldRange(x, y)
                     && searchField[x, y] is FieldMarks.Ship)
                            Reproduce(new FieldCoords(x, y), searchField);
                }
            }
        }
    }
}
