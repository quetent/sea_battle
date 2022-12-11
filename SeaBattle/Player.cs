namespace SeaBattle
{
    internal class Player
    {
        private readonly string _name;
        public string Name { get { return _name; } }
        
        private readonly Field _defenseField;
        public Field DefenseField { get { return _defenseField; } }

        private readonly Field _attackField;
        public Field AttackField { get { return _attackField; } }

        public Player(string name, string filePath, Field defenseField, Field attackField)
        {
            _name = name;

            _attackField = attackField;

            _defenseField = defenseField;
            _defenseField.ParseFieldFromFile(new FileInfo(filePath));
        }

        public void Turn()
        {
            var command = CommandReader.ReadCommand();

            if (command.Type is CommandsEnum.Attack)
                Game.ExecuteAttackCommand(command, _attackField);
            else if (command.Type is CommandsEnum.Restart || command.Type is CommandsEnum.Stop)
                Game.ExecuteGlobalCommand(command);
            else
                Logger.Log($"< {command.Type} command >", ConsoleColor.DarkRed);
        }
    }
}
