using Microsoft.Xna.Framework.Graphics;


namespace Slay_The_Basilisk_MonoGame.Scripts.Map.Elements.ActorElements.ObstacleElements
{
    internal class ObstacleElement : ActorElement, IObstacle
    {
        public ObstacleElement(Texture2D sprite, Point mapPosition) : base(sprite, mapPosition)
        {
        }
    }
}
