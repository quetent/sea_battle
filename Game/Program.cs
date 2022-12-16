namespace Game
{
    internal class Program
    {
        static void Main()
        {
            var playerField1 = new Field();
            var playerField2 = new Field();

            var fieldFilepath1 = $@"{FilesLocationDir}\{FieldFilename}1.txt";
            var fieldFilepath2 = $@"{FilesLocationDir}\{FieldFilename}2.txt";

            var player1 = new Player("Awen",
                                    fieldFilepath1,
                                    playerField1,
                                    playerField2);

            var player2 = new Player("Darya",
                                    fieldFilepath2,
                                    playerField2,
                                    playerField1);

            new Game(player1, player2).Start();
        }
    }
}