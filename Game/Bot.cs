namespace Game
{
    public class Bot : Player
    {
        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            _lastAttackCoords = _attackField.GetRandomNotHitOrMissCoords();

            EnterAttackCoords();

            return new Command(CommandsEnum.SimpleAttack);
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
