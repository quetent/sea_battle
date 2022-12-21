namespace FieldPatternFileCreator
{
    internal class Settings
    {
        public static readonly string WindowTitle = "Pattern creator";

        public static readonly int FileCreatingTimeInMs = 1000;

        public static readonly int ConsoleWindowHeight = 30;
        public static readonly int ConsoleWindowWidth = 70;

        static Settings()
        {
            Title = WindowTitle;

            WindowHeight = ConsoleWindowHeight;
            WindowWidth = ConsoleWindowWidth;

            SetConsoleBuffers();
        }

        private static void SetConsoleBuffers()
        {
            BufferHeight = WindowHeight;
            BufferWidth = WindowWidth;
        }
    }
}
