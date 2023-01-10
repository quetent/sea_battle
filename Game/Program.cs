using System.Reflection;

namespace Game
{
    internal class Program
    {
        static void Main()
        {
            var (name1, name2) = GetPlayersNames("Awen");

            var (path1, path2) = GetFieldFilesPaths($@"{FilesLocationDir}\{FieldFilename}1.txt",
                                                    $@"{FilesLocationDir}\{FieldFilename}2.txt");

            var (field1, field2) = GetPlayersFields();

            var (player1, player2) = GetPlayers(name1, name2, 
                                                path1, path2, 
                                                field1, field2);  

            var game = new Game(player1, player2);

            game.Start();
        }

        private static (Player, Player) GetPlayers(string name1, string name2,
                                                   string path1, string path2,
                                                   Field field1, Field field2)
        {
            var player1 = new Player(name1, path1, field1, field2);
            Player player2;

            if (IsVersusBot)
                player2 = new Bot(name2, path2, field2, field1);
            else
                player2 = new Player(name2, path2, field2, field1);

            return (player1, player2);
        }

        private static (string, string) GetFieldFilesPaths(string path1, string path2)
        {
            return (path1, path2);
        }

        private static (Field, Field) GetPlayersFields()
        {
            return (new Field(), new Field());
        }

        private static (string, string) GetPlayersNames(string name1, string name2 = "Bot")
        {
            return (name1, name2);
        }
    }
}