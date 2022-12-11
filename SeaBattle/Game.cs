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
                Drawer.DrawFields(currentPlayer.AttackField, currentPlayer.DefenseField);
                currentPlayer.Turn();
                Drawer.DrawFields(currentPlayer.AttackField, currentPlayer.DefenseField);
                Drawer.DrawLine();
            }
        }

        public static void ExecuteGlobalCommand(Command command)
        {
            switch (command.Type)
            {
                case CommandsEnum.Restart:
                    break;
                case CommandsEnum.Stop:
                    break;
            }
        }

        public static void ExecuteAttackCommand(Command command, Field attackField)
        {
            Field.ProduceAttack(attackField);
        }

        public void Restart()
        {

        }
    }
}
