namespace Game
{
    internal class Program
    {
        static void Main()
        {
            var player1Name = "Awen";
            string player2Name;

            if (IsVersusBot)
                player2Name = "Bot";
            else
                player2Name = "Darya";

            var playerField1 = new Field();
            var playerField2 = new Field();

            var fieldFilepath1 = $@"{FilesLocationDir}\{FieldFilename}1.txt";
            var fieldFilepath2 = $@"{FilesLocationDir}\{FieldFilename}2.txt";

            var player1 = new Player(player1Name,
                                     fieldFilepath1,
                                     playerField1,
                                     playerField2);

            Player player2;

            if (IsVersusBot)
                player2 = new Bot(player2Name,
                                  fieldFilepath2,
                                  playerField2,
                                  playerField1);
            else
                player2 = new Player(player2Name,
                                     fieldFilepath2,
                                     playerField2,
                                     playerField1);

            new Game(player1, player2).Start();
        }
    }
}