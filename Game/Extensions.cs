namespace Game
{
    public static class IntExtensions
    {
        public static bool InRange(this int number, int bottom, int top)
        {
            return number >= bottom && number <= top;
        }

        public static int RoundToNearest(this int number, int numberOrder)
        {
            return number + (numberOrder - number % numberOrder);
        }
    }

    public static class StringExtensions
    {
        public static string Copy(this string originalStr)
        {
            return (string)originalStr.Clone();
        }

        public static bool ConsistsOfLetters(this string str)
        {
            return str.ToCharArray().All(x => char.IsLetter(x));
        }

        public static bool ConsistsOfDigits(this string str)
        {
            return str.ToCharArray().All(x => char.IsDigit(x));
        }
    }

    public static class CharExtensions
    {
        public static bool InRange(this char character, int bottom, int top)
        {
            return ((int)character).InRange(bottom, top);
        }
    }

    public static class ListExtensions
    {
        public static List<T> Copy<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }
}
