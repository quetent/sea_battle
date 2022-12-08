namespace SeaBattle
{
    internal class Game
    {
        public void Start()
        {
            var field1 = new Field();
            field1.ParseFieldFromFile(new FileInfo(@"D:\\projects\\cs projects\\SeaBattle\\SeaBattle\\playerFieldTesting.txt"));

            var field2 = new Field();
            field2.ParseFieldFromFile(new FileInfo(@"D:\\projects\\cs projects\\SeaBattle\\SeaBattle\\playerFieldTesting.txt"));

            Drawer.DrawFields(field1, field2);
            //Drawer.DrawField(field1);
        }
    }
}
