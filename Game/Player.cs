namespace Game
{
    public class Player
    {
        protected readonly string _name;
        public string Name { get { return _name; } }

        protected FieldCoords _lastAttackCoords;
        public FieldCoords LastAttackCoords { get { return _lastAttackCoords; } }

        protected readonly Field _defenseField;
        public Field DefenseField { get { return _defenseField; } }

        protected readonly Field _attackField;
        public Field AttackField { get { return _attackField; } }

        protected readonly FileInfo _fieldFile;
        public FileInfo FilePath { get { return _fieldFile; } }

        public Player(string name, string filePath, Field defenseField, Field attackField)
        {
            _name = name;

            _attackField = attackField;

            _fieldFile = new FileInfo(filePath);

            _defenseField = defenseField;
            _defenseField.ParseFieldFromFile(_fieldFile);
        }

        public virtual Command Turn()
        {
            var command = CommandReader.ReadCommand(out string rawOutput);

            if (command.Type is CommandsEnum.SimpleAttack)
            {
                var attackCoords = new FieldCoords(rawOutput[0], int.Parse(rawOutput[1..]));

                if (_attackField[attackCoords] is FieldMarks.Empty
                 || _attackField[attackCoords] is FieldMarks.Ship)
                    _lastAttackCoords = attackCoords;
                else
                    command = new Command(CommandsEnum.Invalid);
            }

            return command;
        }
    }
}
