namespace SeaBattle
{
    internal class Program
    {
        static void Main()
        {
            var player1Field = new Field();
            var player2Field = new Field();

            var player1 = new Player("Awen",
                                    @"..\\..\\..\\..\\SeaBattle\\playerFieldTesting1.txt",
                                    player1Field,
                                    player2Field);

            var player2 = new Player("Darya",
                                    @"..\\..\\..\\..\\SeaBattle\\playerFieldTesting2.txt",
                                    player2Field,
                                    player1Field);

            new Game(player1, player2).Start();
        }
    }
}