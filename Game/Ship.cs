using System.Drawing;

namespace Game
{
    public class Ship
    {
        private readonly Dictionary<FieldCoords, bool> _location;

        private int _hits;
        public int Hits { get { return _hits; } }

        public int Size { get { return _location.Count; } }

        public Ship(FieldCoords coords, FieldMarks[,] searchField)
        {
            _location = new Dictionary<FieldCoords, bool>();
            Reproduce(coords, searchField);
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (var part in _location)
            {
                var condition = part.Value ? "intact" : "damaged";
                result += $"{part.Key}: {condition}, ";
            }            

            return $"[ {result} ]";
        }

        public bool IsDestroyed()
        {
            return Size == _hits;
        }

        public bool Belongs(FieldCoords coords)
        {
            return Belongs(coords.X, coords.Y);
        }

        public void Hit(FieldCoords coords)
        {
            foreach (var part in _location)
            {
                if (part.Key.X == coords.X
                 && part.Key.Y == coords.Y)
                {
                    if (!part.Value)
                        throw new ArgumentException("ship part already damaged", nameof(coords));
                    else
                    {
                        _location[part.Key] = true;
                        _hits++;
                    }
                }
            }
        }

        public List<FieldCoords> GetDestroyFrame()
        {
            var frame = new List<FieldCoords>();

            foreach (var _coords in _location)
                for (int x = _coords.Key.X - 1; x <= _coords.Key.X + 1; x++)
                    for (int y = _coords.Key.Y - 1; y <= _coords.Key.Y + 1; y++)
                        if (Field.IsInFieldRange(x, y) && !Belongs(x, y))
                            frame.Add(new FieldCoords(x, y));

            return frame;
        }

        private void Reproduce(FieldCoords coords, FieldMarks[,] searchField)
        {
            if (Belongs(coords))
                return;

            _location.Add(coords, true);

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

        private bool Belongs(int x, int y)
        {
            foreach (var _coords in _location)
                if (x == _coords.Key.X
                 && y == _coords.Key.Y)
                    return true;

            return false;
        }
    }
}
