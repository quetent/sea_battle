namespace Game
{
    public static class Settings
    {
        public const string WindowTitle = "Sea Battle";

        public const int WindowWidthReserve = 0;
        public const int WindowHeightReserve = 6;

        public const string StopCommand = "STOP";
        public const string RestartCommand = "RESTART";

        public const string FieldFilename = "fieldPlayer";
        public const string FilesLocationDir = @"..\..\..\..\";

        public const string SelfCaption = "< You >";
        public const string OpponentCaption = "< Enemy >";

        public const int BotCommandEnteringInMs = 350;
        public const int CommandDelayInMs = 150;
        public const int RestartingTimeInMs = 1000;

        public const int LettersCount = 7;
        public const int NumbersCount = 7;

        public const int AlphabetSize = 26;
        public const int NumbersSize = 10;

        public const int CharacterOffset = 65;

        public const bool IsFullDrawing = true;
        public const bool IsVersusBot = true;

        public static readonly int AxesIndent = 1;
        public static readonly int IndentFromDigit = 1;
        public static readonly int IndentBetweenFields = 5;
        public static readonly int IndentBetweenCaptions = IndentBetweenFields;

        public const ConsoleColor DigitsColor = ConsoleColor.White;
        public const ConsoleColor LettersColor = ConsoleColor.DarkBlue;
        public const ConsoleColor PlayerCaptionColor = ConsoleColor.DarkYellow;

        public const ConsoleColor WinnerColor = ConsoleColor.Cyan;
        public const ConsoleColor PlayerTurnColor = ConsoleColor.DarkYellow;

        public static void ResetConsoleForegroundColor() => ConsoleForegroundColor = ConsoleColor.White;
        public static ConsoleColor ConsoleForegroundColor 
        { 
            get { return ConsoleForegroundColor; } 
            set { ForegroundColor = value; } 
        }

        static Settings()
        {
            Title = WindowTitle;

            int windowIndent;
            var delta = OpponentCaption.Length - LettersCount;
            if (delta > 0)
            {
                IndentBetweenFields = delta + IndentBetweenFields;
                windowIndent = IndentBetweenFields;
            }
            else
            {
                IndentBetweenCaptions = Math.Abs(delta) + IndentBetweenFields;
                windowIndent = LettersCount;
            }

            SetConsoleSize(windowIndent);
            SetConsoleBuffers();
        }

        private static void SetConsoleSize(int indent)
        {
            var windowHeight = (NumbersCount + AxesIndent).RoundToNearest(10) * 2
                              + WindowHeightReserve;

            var windowWidth = indent.RoundToNearest(15) * 2
                             + Math.Max(LettersCount, SelfCaption.Length - LettersCount)
                             + WindowWidthReserve;

            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
        }   

        private static void SetConsoleBuffers()
        {
            BufferHeight = WindowHeight;
            BufferWidth = WindowWidth;
        }
    }
}
