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
            Drawer.DrawFields(_player2.DefenseField, _player1.DefenseField); // pl1 turn
        }


    }
}
