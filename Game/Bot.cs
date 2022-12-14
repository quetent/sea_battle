using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Game
{
    public class Bot : Player
    {
        private readonly ObservableCollection<FieldCoords> _targetShipHits;
        private bool _isNeedTargetRangeRecalculating;

        private int _startX, _endX, _startY, _endY;

        public Bot(string name, string filePath, Field defenseField, Field attackField) 
            : base(name, filePath, defenseField, attackField)
        {
            _targetShipHits = new();
            SetTargetShipCollectionNotifyer();
        }

        public override Command Turn()
        {
            SetAttackCoords();
            EnterAttackCoords();

            return new Command(CommandsEnum.SimpleAttack);
        }

        public void ResetTargetShipCoords()
        {
            _targetShipHits.Clear();
        }

        private void SetAttackCoords()
        {
            if (_attackField.IsHit(_lastAttackCoords))
                _targetShipHits.Add(_lastAttackCoords);

            if (_targetShipHits.Count > 0
             && _attackField.GetShipByCoords(_targetShipHits[^1], out Ship? ship)
             && !ship!.IsDestroyed())
            {
                SetTargetShipRange();
                _lastAttackCoords = _attackField.GetRegionalFreeCoords(_startX, _endX, _startY, _endY);
            }
            else
                _lastAttackCoords = _attackField.GetRandomFreeCoords();
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

        private void SetTargetShipRange()
        {
            if (_isNeedTargetRangeRecalculating)
                CalcuteTargetShipRange();
        }

        private void CalcuteTargetShipRange()
        {
            (_startX, _startY) = FindTargetShipMinCoords();
            (_endX, _endY) = FindTargetShipMaxCoords();

            _isNeedTargetRangeRecalculating = false;
        }

        private (int minX, int minY) FindTargetShipMinCoords()
        {
            int x = int.MaxValue, y = int.MaxValue;

            foreach (var coords in _targetShipHits)
            {
                x = coords.X > x ? x : coords.X;
                y = coords.Y > y ? y : coords.Y;
            }
            
            return (x, y);
        }

        private (int maxX, int maxY) FindTargetShipMaxCoords()
        {
            int x = int.MinValue, y = int.MinValue;

            foreach (var coords in _targetShipHits)
            {
                x = coords.X < x ? x : coords.X;
                y = coords.Y < y ? y : coords.Y;
            }

            return (x, y);
        }

        private void SetTargetShipCollectionNotifyer()
        {
            _targetShipHits.CollectionChanged
                += delegate (object? sender, NotifyCollectionChangedEventArgs e)
                { _isNeedTargetRangeRecalculating = true; };
        }
    }
}
