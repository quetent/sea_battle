namespace Game
{
    internal class Bot : Player
    {
        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            return base.Turn();
        }
    }
}
