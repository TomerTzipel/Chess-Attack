
namespace ChessOut.Scenes
{
    //The main menu, has a button to start a new run, continue an existing run (within an execution of the program)
    //and to exit the game
    public class MainMenuScene : MenuScene
    {
        private Button _startButton;
        private Button _continueButton;
        private Button _quitButton;
        private TextBox _gameTitle;
        
        public MainMenuScene()
        {
            //Positions all the UI elements on the screen
            _gameTitle = new TextBox(new Vector2((GameManager.WindowWidth / 2)-64,200),128,64,5f,"CHESS OUT",Color.Purple);

            float buttonScale = 2f;
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth/2 - (int)(buttonSprite.Width * buttonScale) / 2;
            int _startButtonY = 400;
            _startButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonScale, buttonSprite,"Start");
            _continueButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height * buttonScale + 50), buttonScale, buttonSprite, "Continue");
            _quitButton = new Button(new Vector2(_startButtonX, _startButtonY + ((buttonSprite.Height * buttonScale + 50)*2)), buttonScale, buttonSprite, "Quit");

            //Subscribes to the buttons events
            _startButton.OnClick += StartRun;
            _continueButton.OnClick += ContinueRun;
            _quitButton.OnClick += QuitGame;

            _buttons = new Button[] { _startButton, _continueButton, _quitButton};
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _gameTitle.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        public override void EnterScene()
        {
            //Deactivates the continue button if there isn't an active run
            _continueButton.IsClickable = RunManager.IsRunActive;
        }

        private void StartRun(object sender,EventArgs eventArgs)
        {
            RunManager.IsRunActive = false;
            GameManager.ChangeScene(SceneType.Game);
        }
        private void ContinueRun(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.Game);
        }

        private void QuitGame(object sender, EventArgs eventArgs)
        {
            GameManager.ExitGame();
        }

    }
}
