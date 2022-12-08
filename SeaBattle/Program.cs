namespace SeaBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("Awen",
                                    @"..\\..\\..\\..\\SeaBattle\\playerFieldTesting1.txt");
            var player2 = new Player("Darya",
                                    @"..\\..\\..\\..\\SeaBattle\\playerFieldTesting2.txt");

            new Game(player1, player2).Start();
        }
    }
}