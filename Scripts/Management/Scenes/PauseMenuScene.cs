using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Slay_The_Basilisk_MonoGame.Scripts.Management.Scenes
{
    internal class PauseMenuScene : MenuScene
    {
        private Button _resumeButton;
        private Button _mainMenuButton;
        public PauseMenuScene()
        {
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth / 2 - buttonSprite.Width / 2;
            int _startButtonY = 100;
            _resumeButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonSprite, "Resume");
            _mainMenuButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height + 50), buttonSprite, "Main Menu");


            _resumeButton.Click += Resume;
            _mainMenuButton.Click += GoToMainMenu;

            _buttons = new Button[] { _resumeButton, _mainMenuButton };
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        private static void Resume(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.Game);
        }
        private static void GoToMainMenu(object sender, EventArgs eventArgs)
        {
            //Only if a run is active
            GameManager.ChangeScene(SceneType.MainMenu);
        }

    }
}
