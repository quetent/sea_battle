namespace Game
{
    public class Bot : Player
    {
        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            _lastAttackCoords = _attackField.GetRandomNotHitOrMissCoords();

            return new Command(CommandsEnum.SimpleAttack);
        }
    }
}
