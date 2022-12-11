namespace SeaBattle
{
    internal static class CommandReader
    {
        public static Command ReadCommand(out string rawOutput)
        {
            ConsoleForegroundColor = ConsoleColor.White;
            Write(">>> "); //

            rawOutput = ReadLine().ToUpper();
            
            return ParseCommand(rawOutput);
        }

        private static Command ParseCommand(string? command)
        {
            if (IsCommandEmpty(command))
                return new Command(CommandsEnum.Empty);

            if (IsAttackSignatureCommand(command))
            {
                if (command[0].InRange(0 + CharacterOffset, CharacterOffset + LettersCount - 1)
                 && int.Parse(command[1..]).InRange(0, NumbersCount - 1))
                    return new Command(CommandsEnum.SimpleAttack);
                else
                    return new Command(CommandsEnum.Invalid);
            }

            if (command == RestartCommand)
                return new Command(CommandsEnum.Restart);
            if (command == StopCommand)
                return new Command(CommandsEnum.Stop);

            return new Command(CommandsEnum.Unknown);
        }

        private static bool IsAttackSignatureCommand(string command)
        {
            return command.Length >= 2 
                && char.IsLetter(command[0]) 
                && command[1..].ConsistsOfDigits();
        }

        private static bool IsCommandEmpty(string? command)
        {
            return string.IsNullOrWhiteSpace(command) || string.IsNullOrEmpty(command);
        }
    }
}
