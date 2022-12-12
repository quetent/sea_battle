using System.ComponentModel;

namespace SeaBattle
{
    internal class Game
    {
        private readonly Player _player1;
        public string Player1 { get { return _player1.Name; } }

        private readonly Player _player2;
        public string Player2 { get { return _player2.Name; } }

        private bool _isStopped;

        private static readonly Random random = new(); 

        public Game(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Start()
        {
            do
            {
                var switching = random.Next(0, 2);
                var attackPlayer = SwitchPlayers(ref switching);
                var isNeedSwitching = false;
                var isRestarted = false;

                while (!IsGameEnded())
                {
                    if (isNeedSwitching)
                        attackPlayer = SwitchPlayers(ref switching);

                    OpenTurn(attackPlayer);

                    var turnCommand = attackPlayer.Turn();
                    WaitTime(CommandDelayInMs);

                    ProcessCommand(turnCommand, attackPlayer, ref isNeedSwitching);

                    if (IsGameBreak(turnCommand))
                    {
                        isRestarted = turnCommand.Type is CommandsEnum.Restart;
                        break;
                    }

                    CloseTurn(attackPlayer);
                }

                if (!isRestarted && !_isStopped)
                    Restart();
            } while (!_isStopped);

            Drawer.DrawLine();
            Logger.LogGameStopping();
        }

        public void Restart()
        {
            Drawer.Erase();
            Logger.LogRefreshFieldsReady();
            CommandReader.WaitButtonPress();
            Logger.LogGameRestarting();
            WaitTime(RestartingTimeInMs);
            Drawer.Erase();
            RefreshPlayerFields();
        }

        public void Stop()
        {
            _isStopped = true;
        }

        private static void Wait()
        {
            CommandReader.WaitButtonPress("... ");
        }

        private void ProcessCommand(Command command, Player attacker, ref bool isNeedSwitching)
        {
            switch (command.Type)
            {
                case CommandsEnum.SimpleAttack:
                    ExecuteAttackCommand(attacker.LastAttackCoords, attacker.AttackField, ref isNeedSwitching);
                    break;
                case CommandsEnum.Restart:
                    Restart();
                    break;
                case CommandsEnum.Stop:
                    Stop();
                    break;
                case CommandsEnum.Invalid:
                    isNeedSwitching = false;
                    Logger.LogInvalidCommand();
                    break;
                case CommandsEnum.Empty:
                    Logger.LogEmptyCommand();
                    break;
                default:
                    Logger.LogUnknownCommand();
                    break;
            }
        }

        private static void ExecuteAttackCommand(FieldCoords coords, Field defenseField, ref bool isNeedSwitching)
        {
            defenseField.ProduceAttack(coords, out isNeedSwitching);
        }

        private Player SwitchPlayers(ref int switching)
        {
            var attackPlayer = switching % 2 == 0 ? _player1 : _player2;

            switching++;

            return attackPlayer;
        }

        private bool IsGameEnded()
        {
            if (_player1.DefenseField.IsAllShipsDestroyed() 
             || _player2.DefenseField.IsAllShipsDestroyed())
                return true;

            return false;
        }

        private static bool IsGameBreak(Command command)
        {
            return command.Type is CommandsEnum.Stop
                || command.Type is CommandsEnum.Restart;
        }

        private void RefreshPlayerFields()
        {
            _player1.DefenseField.ParseFieldFromFile(_player1.FilePath);
            _player2.DefenseField.ParseFieldFromFile(_player2.FilePath);
        }

        private static void WaitTime(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        private static void OpenTurn(Player player)
        {
            Logger.LogPlayerTurn(player);
            Drawer.DrawLine();

            Drawer.DrawFields(player.AttackField, player.DefenseField);
            Drawer.DrawLine();
        }

        private static void CloseTurn(Player player)
        {
            Drawer.DrawLine();
            Drawer.DrawFields(player.AttackField, player.DefenseField);
            Wait();
            Drawer.Erase();
        }

    }
}
