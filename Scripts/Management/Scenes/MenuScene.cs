using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public abstract class MenuScene : Scene
    {
        protected Button[] _buttons;

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in _buttons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Button button in _buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }
        }
    }
}
