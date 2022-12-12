namespace SeaBattle
{
    internal readonly struct FieldCoords
    {
        private readonly int _x;
        public int X { get { return _x; } }

        private readonly int _y;
        public int Y { get { return _y; } }

        public FieldCoords(char x, int y)
        {
            _x = ParseX(x);
            _y = ParseY(y);
        }

        public FieldCoords(int x, int y)
        {
            _x = ParseX(x);
            _y = ParseY(y);
        }

        public override string ToString()
        {
            return $"({_y}, {_x})";
        }

        public FieldCoords Reverse()
        {
            return new FieldCoords(_y, _x);
        }

        public static bool IsValidCoordX(int x)
        {
            return x.InRange(0, LettersCount - 1);
        }

        public static bool IsValidCoordY(int y)
        {
            return y.InRange(0, NumbersCount - 1);
        }

        private static bool IsValidCoordX(char x)
        {
            return x.InRange(CharacterOffset, CharacterOffset + AlphabetSize - 1);
        }

        private static int ParseX(int x)
        {
            if (!x.InRange(0, NumbersCount - 1))
                throw new ArgumentException($"coordinate character should be 0-{NumbersCount - 1}", nameof(x));

            return x;
        }

        private static int ParseX(char x)
        {
            if (!IsValidCoordX(x))
                throw new ArgumentException("coordinate character should be A-Z", nameof(x));

            return x - 65;
        }

        private static int ParseY(int y)
        {
            if (!IsValidCoordY(y))
                throw new ArgumentException($"coordinate digit should be 0-{NumbersCount - 1}", nameof(y));

            return y;
        }
    }
}
