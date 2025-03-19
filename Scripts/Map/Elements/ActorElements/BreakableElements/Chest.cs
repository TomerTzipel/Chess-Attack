
namespace ChessOut.MapSystem.Elements
{
    //A breakable item that only breaks if the player has a key
    public class Chest : BreakableElement
    {
        public Chest(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Chest), 1, mapPosition) { }

        public override bool TakeDamage(int damage)
        {
            
            if (RunManager.Player.UseKey())
            {
                return base.TakeDamage(damage);
            }
            return false;
        }

        public override void Die()
        {
            LootManager.GivePlayerLootFromOrigin(LootOrigin.Chest);
        }
    }
}
