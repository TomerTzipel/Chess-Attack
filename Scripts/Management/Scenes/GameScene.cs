using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;


namespace ChessOut.Scenes
{
    public class MovementEventArgs : EventArgs
    {
        public Direction Direction { get; set; }
    }
    public class GameScene : Scene
    {
        public GameScene() 
        {
            //Mapping player controls to the input map.
            _inputMap.AddAction(new KeyboardAction(Keys.W, ButtonState.Pressed, new MovementEventArgs { Direction = Direction.Up })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.S, ButtonState.Pressed, new MovementEventArgs { Direction = Direction.Down })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.A, ButtonState.Pressed, new MovementEventArgs { Direction = Direction.Left })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.D, ButtonState.Pressed, new MovementEventArgs { Direction = Direction.Right })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.Escape, ButtonState.Pressed)).Action += OnPause;
            _inputMap.AddAction(new KeyboardAction(Keys.Q, ButtonState.Pressed)).Action += DrinkPotion;
        }

        public override void EnterScene()
        {
            if (!RunManager.IsRunActive)
            {
                StartNewRun();
            }
        }

        private void StartNewRun()
        {
            RunManager.StartRun(GameData.Maps.Count);
        }

        public override void Update(GameTime gameTime)
        {
            _inputMap.CheckActions();
            RunManager.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            RunManager.Draw(gameTime, spriteBatch);
        }
        
        public void OnPause(object sender,EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.Pause);
        }
        public void DrinkPotion(object sender, EventArgs eventArgs)
        {
            RunManager.Player.DrinkHealthPotion();
        }
    }
}
