using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Slay_The_Basilisk_MonoGame
{
    public class MainMenuScene : MenuScene
    {
        private Button _startButton;
        private Button _continueButton;
        private Button _quitButton;
        
        public MainMenuScene()
        {
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth/2 - buttonSprite.Width/2;
            int _startButtonY = 100;
            _startButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonSprite,"Start");
            _continueButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height + 50), buttonSprite, "Continue");
            _quitButton = new Button(new Vector2(_startButtonX, _startButtonY + ((buttonSprite.Height + 50)*2)), buttonSprite, "Quit");

            _startButton.Click += StartRun;
            _continueButton.Click += ContinueRun;
            _quitButton.Click += QuitGame;

            _buttons = new Button[] { _startButton, _continueButton, _quitButton};
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        private static void StartRun(object sender,EventArgs eventArgs)
        {
            RunManager.IsRunActive = false;
            GameManager.ChangeScene(SceneType.Game);
        }
        private static void ContinueRun(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.Game);
        }

        private static void QuitGame(object sender, EventArgs eventArgs)
        {
            GameManager.Exit();
        }

    }
}
