



using System.Diagnostics;

namespace Slay_The_Basilisk_MonoGame
{
    public class PlayerElement : CharacterElement
    {
        //Add inventory stuff boi
        public Point MapPosition
        {
            get { return _mapPosition; }
            set { _mapPosition = value; }
        }

        public PlayerElement() : base(AssetsManager.GetAsset(Asset.Player), AssetsManager.GetAsset(Asset.PlayerCD), new Point(), GameData.PlayerStats) 
        {
            _timer.Start(0.001d);
        }
        
        public override void Act(Direction direction)
        {
            if (_isOnCooldown) return;

            Point targetPosition = new Point(_mapPosition).MovePointInDirection(direction);
            MapElement elementInDirection = RunManager.CurrentLevel.Map.ElementAt(targetPosition);

            if (elementInDirection == null || elementInDirection is IObstacle) return;

            if (elementInDirection is EmptyElement)
            {
                Move(direction);
                return;
            }

            if(elementInDirection is IHitable hitable)
            {
                Attack(hitable,direction);
            }
        }

        public override void Die()
        {
            RunManager.EndRun(false);
        }
    }
}
