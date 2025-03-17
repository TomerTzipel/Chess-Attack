
using System.Collections.Generic;


namespace Slay_The_Basilisk_MonoGame
{
    public class Rook : EnemyElement
    {
        public Rook(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Rook),AssetsManager.GetAsset(Asset.RookCD), mapPosition,5f, GameData.RookStats) { }

        protected override List<Direction> CalculateAvailableDirections()
        {
            List<Direction> availableDirections = new List<Direction>(4);

            if(Point.Distance(_mapPosition,RunManager.PlayerPosition) <= _aggroRange)
            {
                CalculateDirectionsToPlayer(availableDirections);
            }
            else
            {
                availableDirections.Add(Direction.Up);
                availableDirections.Add(Direction.Left);
                availableDirections.Add(Direction.Right);
                availableDirections.Add(Direction.Down);
            }
            
            return availableDirections;
        }
        protected override void CalculateDirectionsToPlayer(List<Direction> directions)
        {
            Point playerPosition = new Point(RunManager.PlayerPosition);

            if (playerPosition.X == _mapPosition.X)
            {
                if (playerPosition.Y > _mapPosition.Y)
                {
                    directions.Add(Direction.Down);
                }
                else
                {
                    directions.Add(Direction.Up);
                }

                return;
            }

            if (playerPosition.Y == _mapPosition.Y)
            {
                if (playerPosition.X > _mapPosition.X)
                {
                    directions.Add(Direction.Right);
                }
                else
                {
                    directions.Add(Direction.Left);
                }

                return;
            }

            if (playerPosition.X > _mapPosition.X)
            {
                directions.Add(Direction.Right);
            }
            else
            {
                directions.Add(Direction.Left);
            }

            if (playerPosition.Y > _mapPosition.Y)
            {
                directions.Add(Direction.Down);
            }
            else
            {
                directions.Add(Direction.Up);
            }
        }
    }
}
