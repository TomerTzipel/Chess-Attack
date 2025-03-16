using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public enum SceneType
    {
        MainMenu,Pause,Game
    }
    public abstract class Scene : IMyDrawable,IMyUpdateable
    {
        protected InputMap _inputMap;

        public Scene() 
        {
            _inputMap = new InputMap();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        //Default is to do nothing
        public virtual void EnterScene() { }


        
    }
}
