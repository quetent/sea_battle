namespace Game
{
    public class Bot : Player
    {
        private static readonly Random _random = new();

        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            var x = Convert.ToChar(_random.Next(0, LettersCount) + CharacterOffset);
            var y = _random.Next(0, NumbersCount);

            _lastAttackCoords = new FieldCoords(x, y);

            return new Command(CommandsEnum.SimpleAttack);
        }
    }
}
