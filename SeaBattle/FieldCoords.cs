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

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }

        private static int ParseX(char x)
        {
            if (!x.InRange(CharacterOffset, CharacterOffset + AlphabetSize - 1))
                throw new ArgumentException("coordinate character should be A-Z", nameof(x));

            return x - 65;
        }

        private static int ParseY(int y)
        {
            if (!y.InRange(0, NumbersSize - 1))
                throw new ArgumentException("coordinate digit should be 0-9", nameof(y));

            return y;
        }
    }
}
