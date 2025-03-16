using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Slay_The_Basilisk_MonoGame
{
    public interface IMyDrawable
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
