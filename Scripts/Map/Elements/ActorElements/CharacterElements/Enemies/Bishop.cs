

using System.Collections.Generic;

namespace Slay_The_Basilisk_MonoGame
{
    public class Bishop : EnemyElement
    {
        public Bishop(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Bishop), AssetsManager.GetAsset(Asset.BishopCD), mapPosition, 5f, GameData.BishopStats) { }

        protected override List<Direction> CalculateAvailableDirections()
        {
            List<Direction> availableDirections = new List<Direction>(4);

            if (Point.Distance(_mapPosition, RunManager.PlayerPosition) <= _aggroRange)
            {
                CalculateDirectionsToPlayer(availableDirections);
            }
            else
            {
                availableDirections.Add(Direction.NE);
                availableDirections.Add(Direction.NW);
                availableDirections.Add(Direction.SW);
                availableDirections.Add(Direction.SE);
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
                    directions.Add(Direction.SE);
                    directions.Add(Direction.SW);
                }
                else
                {
                    directions.Add(Direction.NE);
                    directions.Add(Direction.NW);
                }
                return;
            }

            if (playerPosition.Y == _mapPosition.Y)
            {
                if (playerPosition.X > _mapPosition.X)
                {
                    directions.Add(Direction.SE);
                    directions.Add(Direction.NE);
                }
                else
                {
                    directions.Add(Direction.SW);
                    directions.Add(Direction.NW);
                }
                return;
            }

            if(playerPosition.X > _mapPosition.X && playerPosition.Y > _mapPosition.Y)
            {
                directions.Add(Direction.SE);
                return;
            }
            if (playerPosition.X > _mapPosition.X && playerPosition.Y < _mapPosition.Y)
            {
                directions.Add(Direction.NE);
                return;
            }
            if (playerPosition.X < _mapPosition.X && playerPosition.Y > _mapPosition.Y)
            {
                directions.Add(Direction.SW);
                return;
            }
            if (playerPosition.X < _mapPosition.X && playerPosition.Y < _mapPosition.Y)
            {
                directions.Add(Direction.NW);
            }
        }
    }
}
