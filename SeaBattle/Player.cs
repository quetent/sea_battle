namespace SeaBattle
{
    internal class Player
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private FieldCoords _lastAttackCoords;
        public FieldCoords LastAttackCoords { get { return _lastAttackCoords; } }

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

        public Command Turn()
        {
            var command = CommandReader.ReadCommand(out string rawOutput);

            if (command.Type is CommandsEnum.SimpleAttack)
            {
                var attackCoords = new FieldCoords(rawOutput[0], int.Parse(rawOutput[1..]));

                var attackX = attackCoords.X;
                var attackY = attackCoords.Y;

                if (_attackField[attackY, attackX] is FieldMarks.Empty
                 || _attackField[attackY, attackX] is FieldMarks.Ship)
                    _lastAttackCoords = attackCoords;
                else
                    command = new Command(CommandsEnum.Invalid);
            }

            return command;
        }
    }
}
