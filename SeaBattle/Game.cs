namespace SeaBattle
{
    internal class Game
    {
        public void Start()
        {
            var field = new Field();
            field.ParseFieldFromFile(new FileInfo(@"D:\\projects\\cs projects\\SeaBattle\\SeaBattle\\playerFieldExample.txt"));
        }
    }
}
