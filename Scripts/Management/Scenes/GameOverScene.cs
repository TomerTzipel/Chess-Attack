﻿
namespace ChessOut.Scenes
{
    //The scene showing the run's result
    //Has a button to return to the main menu or to quit the game
    public class GameOverScene : MenuScene
    {
        private Button _mainMenuButton;
        private Button _quitButton;
        private TextBox _gameResult;

        public GameOverScene()
        {
            //Positions all the UI elements on the screen
            _gameResult = new TextBox(new Vector2((GameManager.WindowWidth / 2) - 64, 200), 128, 64, 5f, "GAME OVER", Color.Red);

            float buttonScale = 2f;
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth / 2 - (int)(buttonSprite.Width * buttonScale) / 2;
            int _startButtonY = 400;
            _mainMenuButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonScale, buttonSprite, "Main Menu");
            _quitButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height * buttonScale + 50), buttonScale, buttonSprite, "Quit");

            //Subscribes to the buttons events
            _mainMenuButton.OnClick += GoToMainMenu;
            _quitButton.OnClick += QuitGame;

            _buttons = new Button[] { _mainMenuButton, _quitButton };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _gameResult.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public override void EnterScene()
        {
            if (RunManager.LastRunResult)
            {
                _gameResult.UpdateText("YOU WON");
                _gameResult.TextColor = Color.Gold;

            }
            else
            {
                _gameResult.UpdateText("GAME OVER");
                _gameResult.TextColor = Color.Red;
            }
        }

        private void GoToMainMenu(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.MainMenu);
        }

        private void QuitGame(object sender, EventArgs eventArgs)
        {
            GameManager.ExitGame();
        }
    }
}
