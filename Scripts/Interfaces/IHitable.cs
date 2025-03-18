
namespace ChessOut.Interfaces
{
    public interface IHitable
    {
        public HealthHandler HealthHandler { get; }

        //Returns False - if hitable is alive, True if hitable is dead
        public bool TakeDamage(int damage);
        public void Die();
    }
}
