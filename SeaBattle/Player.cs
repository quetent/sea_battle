namespace SeaBattle
{
    internal class Player
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private readonly Field _defenseField;
        public Field DefenseField { get { return _defenseField; } }

        public Player(string name, string filePath)
        {
            _name = name;

            _defenseField = new Field();
            _defenseField.ParseFieldFromFile(new FileInfo(filePath));
        }
    }
}
