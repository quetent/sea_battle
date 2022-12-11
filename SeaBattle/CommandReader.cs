﻿namespace SeaBattle
{
    internal static class CommandReader
    {
        public static Command ReadCommand()
        {
            ConsoleForegroundColor = ConsoleColor.White;
            Write(">>> "); //

            return ParseCommand(ReadLine());
        }

        private static Command ParseCommand(string? command)
        {
            if (IsCommandEmpty(command))
                return new Command(CommandsEnum.Empty);

            command = command.ToUpper();

            if (IsAttackSignatureCommand(command))
            {
                if (command[0].InRange(0 + CharacterOffset, CharacterOffset + LettersCount - 1)
                 && int.Parse(command[1..]).InRange(0, NumbersSize - 1))
                    return new Command(CommandsEnum.Attack);
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
            return command.Length > 2 
                && char.IsLetter(command[0]) 
                && command[1..].ConsistsOfDigits();
        }

        private static bool IsCommandEmpty(string? command)
        {
            return string.IsNullOrWhiteSpace(command) || string.IsNullOrEmpty(command);
        }
    }
}
