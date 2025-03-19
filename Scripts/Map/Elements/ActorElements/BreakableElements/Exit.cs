

namespace ChessOut.MapSystem.Elements
{
    //A breakable item that only breaks if the player completed the level
    //Starting the next level
    public class Exit : BreakableElement
    {
        public Exit(Point mapPosition) : base(null, 1, mapPosition)
        {
            if ((mapPosition.X + mapPosition.Y) % 2 == 0)
            {
                _sprite = AssetsManager.Stairs[(int)TileVariant.Dark];
            }
            else
            {
                _sprite = AssetsManager.Stairs[(int)TileVariant.Light];
            }
            
        }

        public override bool TakeDamage(int damage)
        {
            if(EnemyElement._enemies.Count == 0)
            {
               return base.TakeDamage(damage);
            }
            return false;
        }

        //Rewards for finishing the level and moving to the next level
        public override void Die()
        {
            LootManager.GivePlayerLootFromOrigin(LootOrigin.Level);
            RunManager.NextLevel();
        }
    }
}
