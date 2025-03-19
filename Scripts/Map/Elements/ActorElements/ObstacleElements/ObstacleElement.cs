using Microsoft.Xna.Framework.Graphics;


namespace ChessOut.MapSystem.Elements
{
    //An element that blocks movement from character elements
    //There are none currently as I didn't have time to add cosmetics
    public class ObstacleElement : ActorElement, IObstacle
    {
        public ObstacleElement(Texture2D sprite, Point mapPosition) : base(sprite, mapPosition) { }
    }
}
