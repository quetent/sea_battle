namespace Game
{
    internal static class IntExtensions
    {
        public static bool InRange(this int number, int bottom, int top)
        {
            return number >= bottom && number <= top;
        }
    }

    internal static class StringExtensions
    {
        public static bool ConsistsOfLetters(this string str)
        {
            return str.ToCharArray().All(x => char.IsLetter(x));
        }

        public static bool ConsistsOfDigits(this string str)
        {
            return str.ToCharArray().All(x => char.IsDigit(x));
        }
    }

    internal static class CharExtensions
    {
        public static bool InRange(this char character, int bottom, int top)
        {
            return ((int)character).InRange(bottom, top);
        }
    }

    internal static class ListExtensions
    {
        public static List<T> Copy<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }
}
