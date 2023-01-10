using System.ComponentModel;

namespace Game
{
    public class Game
    {
        private int _gamesCount;

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
            while (!_isStopped)
            {
                var switching = random.Next(0, 2);
                var attackPlayer = SwitchPlayers(ref switching);
                var isGameEnded = IsGameEnded();
                var isNeedSwitching = false;
                var isNeedRestarting = false;

                while (!isGameEnded)
                {
                    if (isNeedSwitching)
                        attackPlayer = SwitchPlayers(ref switching);

                    OpenTurn(attackPlayer);

                    var turnCommand = attackPlayer.Turn();
                    WaitTime(CommandDelayInMs);

                    ProcessCommand(turnCommand, attackPlayer, ref isNeedSwitching);

                    if (IsGameBreak(turnCommand))
                    {
                        _gamesCount++;
                        isNeedRestarting = turnCommand.Type is CommandsEnum.Restart;
                        break;
                    }

                    CloseTurn(attackPlayer);

                    isGameEnded = IsGameEnded();
                    if (isGameEnded)
                    {
                        _gamesCount++;
                        DeclareWinner(attackPlayer);
                        isNeedRestarting = true;
                    }
                }

                if (!isNeedRestarting && !_isStopped)
                    Restart();
            }

            InitiateGameStop();
        }

        public void Restart()
        {
            Drawer.Erase();
            Logger.LogPrepareRestartingGame();
            Logger.LogRefreshFieldsReady();

            CommandReader.WaitButtonPress();

            Logger.LogGameRestarting();
            WaitTime(RestartingTimeInMs);

            Drawer.Erase();

            ResetBotTarget();
            RefreshPlayerFields();
        }

        public void Stop()
        {
            _isStopped = true;
        }

        public static void WaitTime(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        private void ResetBotTarget()
        {
            if (IsVersusBot)
            {
                if (_player1 is Bot bot1)
                    bot1.ResetTargetShipCoords();
                if (_player2 is Bot bot2)
                    bot2.ResetTargetShipCoords();
            }
        }

        private static void InitiateGameStop()
        {
            Drawer.DrawLine();
            Logger.LogGameStopping();
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
                    isNeedSwitching = false;
                    Logger.LogEmptyCommand();
                    break;
                default:
                    isNeedSwitching = false;
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

        private void DeclareWinner(Player player)
        {
            Logger.LogWinner(player, _gamesCount);
            CommandReader.WaitButtonPress("... ");
        }
    }
}
