
namespace ChessOut.Scenes
{
    public class MainMenuScene : MenuScene
    {
        private Button _startButton;
        private Button _continueButton;
        private Button _quitButton;
        private TextBox _gameTitle;
        
        public MainMenuScene()
        {
            _gameTitle = new TextBox(new Vector2((GameManager.WindowWidth / 2)-64,200),128,64,5f,"CHESS OUT",Color.Purple);

            float buttonScale = 2f;
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth/2 - (int)(buttonSprite.Width * buttonScale) / 2;
            int _startButtonY = 400;
            _startButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonScale, buttonSprite,"Start");
            _continueButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height * buttonScale + 50), buttonScale, buttonSprite, "Continue");
            _quitButton = new Button(new Vector2(_startButtonX, _startButtonY + ((buttonSprite.Height * buttonScale + 50)*2)), buttonScale, buttonSprite, "Quit");

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
            _gameTitle.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public override void EnterScene()
        {
            _continueButton.IsClickable = RunManager.IsRunActive;
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
