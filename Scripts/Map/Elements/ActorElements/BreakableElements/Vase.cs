

namespace ChessOut.MapSystem.Elements
{
    public class Vase : BreakableElement
    {
        public Vase(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Vase), 5, mapPosition) { }

        public override void Die()
        {
            LootManager.GivePlayerLootFromOrigin(LootOrigin.Vase);
        }
    }
}
