namespace SeaBattle
{
    internal static class Logger
    {
        public static void Log(string message)
        {
            WriteLine(message);
        }

        public static void Log(string message, ConsoleColor color)
        {
            ConsoleForegroundColor = color;
            Log(message);
            ResetConsoleForegroundColor();
        }
    }
}
