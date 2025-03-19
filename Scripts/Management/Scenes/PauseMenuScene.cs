
namespace ChessOut.Scenes
{
    //The pasue menu scene, has a button to return to the game and to go to the main menu
    public class PauseMenuScene : MenuScene
    {
        private Button _resumeButton;
        private Button _mainMenuButton;
        public PauseMenuScene()
        { 
            //Positions all the UI elements on the screen
            float buttonScale = 2f;
            Texture2D buttonSprite = AssetsManager.GetAsset(Asset.Button);
            int _startButtonX = GameManager.WindowWidth / 2 - (int)(buttonSprite.Width * buttonScale) / 2;
            int _startButtonY = 100;
            _resumeButton = new Button(new Vector2(_startButtonX, _startButtonY), buttonScale, buttonSprite, "Resume");
            _mainMenuButton = new Button(new Vector2(_startButtonX, _startButtonY + buttonSprite.Height * buttonScale + 50), buttonScale, buttonSprite, "Main Menu");

            //Subscribes to the buttons events
            _resumeButton.OnClick += Resume;
            _mainMenuButton.OnClick += GoToMainMenu;

            _buttons = new Button[] { _resumeButton, _mainMenuButton };
        }

        private void Resume(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.Game);
        }
        private void GoToMainMenu(object sender, EventArgs eventArgs)
        {
            GameManager.ChangeScene(SceneType.MainMenu);
        }

    }
}
