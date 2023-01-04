namespace Game
{
    public class Bot : Player
    {
        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            SetAttackCoords();
            EnterAttackCoords();

            return new Command(CommandsEnum.SimpleAttack);
        }

        private void SetAttackCoords()
        {
            if (_attackField.IsHit(_lastAttackCoords))
                //_lastAttackCoords = _attackField.GetRandomRegionalEmptyCoord(_lastAttackCoords);
                WriteLine("s");
            else
                _lastAttackCoords = _attackField.GetRandomFreeCell();
        }

        private void EnterAttackCoords()
        {
            Drawer.Draw(">>> ");

            Game.WaitTime(BotCommandEnteringInMs);
            Drawer.Draw(char.ToLower(Convert.ToChar(_lastAttackCoords.X + CharacterOffset)));

            Game.WaitTime(BotCommandEnteringInMs);
            Drawer.Draw(_lastAttackCoords.Y);

            Drawer.DrawLine();
        }
    }
}
