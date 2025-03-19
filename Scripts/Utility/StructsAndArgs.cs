
namespace ChessOut.Utility
{
    //A struct for handling camera size and position on screen, always centerd on the player
    public struct Camera
    {
        public int Width;
        public int Height;
        public Vector2 WorldPosition;
        public Point Origin { get { return RunManager.PlayerPosition - new Point(Width / 2, Height / 2); } }
    }

    public struct CharacterElementStats
    {
        public int MaxHealth;
        public int Damage;
        public double AttackCooldown;
        public double MoveCooldown;
    }

    public class HealthChangeEventArgs : EventArgs
    {
        public int NewHealth;
        public int MaxHealth;
        public HealthChangeMode mode;
    }
    public class MovementEventArgs : EventArgs
    {
        public Direction Direction { get; set; }
    }
}
