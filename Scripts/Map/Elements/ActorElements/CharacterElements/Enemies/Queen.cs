using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOut.MapSystem.Elements.Enemies
{
    public class Queen : EnemyElement
    {
        public Queen(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Queen), AssetsManager.GetAsset(Asset.QueenCD), mapPosition, GameData.QueenRange, GameData.QueenStats) { }

        //Moves in all directions
        protected override List<Direction> CalculateAvailableDirections()
        {
            List<Direction> availableDirections = new List<Direction>(4);

            if (Point.Distance(_mapPosition, RunManager.PlayerPosition) <= _aggroRange)
            {
                CalculateDirectionsToPlayer(availableDirections);
            }
            else
            {
                Direction[] directionsArray = (Direction[])Enum.GetValues(typeof(Direction));
                availableDirections = new List<Direction>(directionsArray);
            }

            return availableDirections;
        }

        //Acts like the rook and the bishop, moving directly to the player if a line or a diagonnal is available,
        //Otherwise moves in the direction of the quadrent but knows which one is better
        //depending on player position to the quradent diagonal created from the queen to the player quadrent
        //In simpler terms she hunts the player down as efficently as possiable without searching algorithms like BFS,DFS, A* etc.
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

            int playerDiff = playerPosition.X - playerPosition.Y;
            int positionDiff = _mapPosition.X - _mapPosition.Y;
            int playerSum = playerPosition.X + playerPosition.Y;
            int positionSum = _mapPosition.X + _mapPosition.Y;
            //++
            if (playerPosition.X > _mapPosition.X && playerPosition.Y > _mapPosition.Y)
            {
                directions.Add(Direction.SE);

                if (playerDiff > positionDiff)
                {
                    directions.Add(Direction.Down);
                    return;
                }

                if (playerDiff < positionDiff)
                {
                    directions.Add(Direction.Right);
                    return;
                } 
            }

            //+-
            if (playerPosition.X > _mapPosition.X && playerPosition.Y < _mapPosition.Y)
            {

                directions.Add(Direction.NE);

                if (playerSum > positionSum)
                {
                    directions.Add(Direction.Up);
                    return;
                }

                if (playerSum < positionSum)
                {
                    directions.Add(Direction.Right);
                    return;
                }
            }

            //-+
            if (playerPosition.X < _mapPosition.X && playerPosition.Y > _mapPosition.Y)
            {

                directions.Add(Direction.SW);

                if (playerSum > positionSum)
                {
                    directions.Add(Direction.Left);
                    return;
                }

                if (playerSum < positionSum)
                {
                    directions.Add(Direction.Down);
                    return;
                }
            }

            //--
            if (playerPosition.X < _mapPosition.X && playerPosition.Y < _mapPosition.Y)
            {
                directions.Add(Direction.NW);

                if (playerDiff > positionDiff)
                {
                    directions.Add(Direction.Left);
                    return;
                }

                if (playerDiff < positionDiff)
                {
                    directions.Add(Direction.Up);
                }
            }
        }
        public override void Die()
        {
            LootManager.GivePlayerLootFromOrigin(LootOrigin.Queen);
            base.Die();
        }

    }
}
