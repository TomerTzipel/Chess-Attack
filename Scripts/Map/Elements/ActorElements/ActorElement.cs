using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace Slay_The_Basilisk_MonoGame
{
    public class ActorElement : MapElement, IMyDrawable
    {
        protected Texture2D _sprite;
        protected Point _mapPosition;

        protected Vector2 WorldPosition
        {
            get 
            {
                Camera camera = RunManager.CurrentLevel.Camera;
                return ((_mapPosition - camera.Origin) * MapElement.ElementSize) + camera.WorldPosition;
            }
        }
        public ActorElement(Texture2D sprite,Point mapPosition) 
        {
            _sprite = sprite;
            _mapPosition = mapPosition;
        }

        public virtual void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, WorldPosition, Color.White);
        }
    }
}
