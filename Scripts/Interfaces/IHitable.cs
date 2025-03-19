
namespace ChessOut.Interfaces
{
    //Interface for every class that maanges HP
    public interface IHitable
    {
        //A class that handles HP
        public HealthHandler HealthHandler { get; }

        //Returns False - if hitable is alive, True if hitable is dead
        public bool TakeDamage(int damage);
        public void Die();
    }
}
