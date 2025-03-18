
namespace ChessOut.MapSystem.Elements
{
    //Static elements that are hitable by the player
    public abstract class BreakableElement : ActorElement, IHitable
    {
        public HealthHandler HealthHandler { get; private set; }

        public BreakableElement(Texture2D sprite,int hitsToBreak,Point mapPosition) : base(sprite, mapPosition)
        {
            HealthHandler = new HealthHandler(hitsToBreak);
            HealthHandler.OnDeath += Die;
        }

        public virtual bool TakeDamage(int damage)
        {
            HealthHandler.ModifyHealth(-1, HealthChangeMode.Current);
            return HealthHandler.IsDead;
        }

        public abstract void Die();  
    }
}
