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

        public static void LogInline(string message)
        {
            Write(message);
        }

        public static void LogInline(string message, ConsoleColor color)
        {
            ConsoleForegroundColor = color;
            LogInline(message);
            ResetConsoleForegroundColor();
        }

        public static void LogRefreshFieldsReady()
        {
            Log("Update fields files");
            Log("Press any button if ready");
        }

        public static void LogGameRestarting()
        {
            Log("Restarting game...");
        }

        public static void LogPrepareRestartingGame()
        {
            Log("Preparing to restart the game");
        }

        public static void LogGameStopping()
        {
            Log("< Game was stopped >", ConsoleColor.DarkRed);
        }

        public static void LogPlayerTurn(Player player)
        {
            Log($"< {player.Name} turn >", PlayerTurnColor);
        }

        public static void LogInvalidCommand()
        {
            Log($"< {CommandsEnum.Invalid} command >", ConsoleColor.DarkRed);
        }

        public static void LogEmptyCommand()
        {
            Log($"< {CommandsEnum.Empty} command >", ConsoleColor.DarkRed);
        }

        public static void LogUnknownCommand()
        {
            Log($"< {CommandsEnum.Unknown} command >", ConsoleColor.DarkRed);
        }

        public static void LogWinner(Player player, int gameNumber)
        {
            Log($"< {player.Name} has won game #{gameNumber} >", WinnerColor);
        }
    }
}
