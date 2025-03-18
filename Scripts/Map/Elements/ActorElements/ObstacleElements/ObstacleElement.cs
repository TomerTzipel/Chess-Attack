using Microsoft.Xna.Framework.Graphics;


namespace ChessOut.MapSystem.Elements
{
    //Didn't have time to add cosmetics as I wanted, but you can understand easily how it would work
    public class ObstacleElement : ActorElement, IObstacle
    {
        public ObstacleElement(Texture2D sprite, Point mapPosition) : base(sprite, mapPosition)
        {
        }
    }
}
