namespace Game
{
    public class Bot : Player
    {
        private FieldCoords _target;

        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField) { }

        public override Command Turn()
        {
            SetAttackCoords();
            EnterAttackCoords();

            return new Command(CommandsEnum.SimpleAttack);
        }

        private int a; // temp
        private void SetAttackCoords()
        {

            if (a == 0) // temp
            {
                CommandReader.ReadCommand(out string rawOutput);
                _target = _lastAttackCoords = new FieldCoords(rawOutput[0], int.Parse(rawOutput[1..]));
                a++;
                return;
            } // temp

            if (_attackField.IsHit(_lastAttackCoords))
                _target = _lastAttackCoords;

            if (_attackField.GetShipByCoords(_target, out Ship? ship)
             && !ship.IsDestroyed())
            {
                _lastAttackCoords = _attackField.GetRandomRegionalFreeCoords(_lastAttackCoords);
            }
            else
            {
                _lastAttackCoords = _attackField.GetRandomFreeCoords();
            }
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
