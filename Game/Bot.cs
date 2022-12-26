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
            

            var coords = _attackField.GetRandomFreeCell();



            _lastAttackCoords = coords;    
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
