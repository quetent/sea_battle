using System.ComponentModel;

namespace SeaBattle
{
    internal class Game
    {
        private readonly Player _player1;
        public string Player1 { get { return _player1.Name; } }

        private readonly Player _player2;
        public string Player2 { get { return _player2.Name; } }

        public Game(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Start()
        {
            while (true)
            {
                var currentPlayer = _player1;
                var playerOpponent = _player2;

                Drawer.DrawFields(currentPlayer.AttackField, currentPlayer.DefenseField);

                var turnCommand = currentPlayer.Turn();

                ProcessCommand(turnCommand, currentPlayer, playerOpponent);

                //Drawer.DrawFields(currentPlayer.AttackField, currentPlayer.DefenseField);
                Drawer.DrawRepeatedEmptyLine(2);
            }
        }

        private void ProcessCommand(Command command, Player attacker, Player defenser)
        {
            switch (command.Type)
            {
                case CommandsEnum.SimpleAttack:
                    ExecuteAttackCommand(attacker.LastAttackCoords, defenser.DefenseField);
                    break;
                case CommandsEnum.Restart:
                    Restart();
                    break;
                case CommandsEnum.Stop:
                    Stop();
                    break;
                case CommandsEnum.Invalid:
                    LogInvalidCommand();
                    break;
                case CommandsEnum.Empty:
                    LogEmptyCommand();
                    break;
                default:
                    LogUnknownCommand();
                    break;
            }
        }

        public static void ExecuteAttackCommand(FieldCoords coords, Field attackField)
        {
            attackField.ProduceAttack(coords);
        }

        public static void LogInvalidCommand()
        {
            Logger.Log($"< {CommandsEnum.Invalid} command >", ConsoleColor.DarkRed);
        }

        public static void LogEmptyCommand()
        {
            Logger.Log($"< {CommandsEnum.Empty} command >", ConsoleColor.DarkRed);
        }

        public static void LogUnknownCommand()
        {
            Logger.Log($"< {CommandsEnum.Unknown} command >", ConsoleColor.DarkRed);
        }

        public void Restart()
        {

        }

        public void Stop()
        {

        }
    }
}
