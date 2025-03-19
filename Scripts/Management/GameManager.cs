
namespace ChessOut
{
    //Manages the game by dictating which scene is currently loaded.
    //Holds all the scenes and manages their running and switching.
    public static class GameManager
    {
        public const int WindowWidth = 1920;
        public const int WindowHeight = 1080;

        private static Game1 _game;
        private static Scene[] _scenes;
        private static SceneType _currentSceneType;

        public static void StartGame(Game1 game)
        {
            //Loads all the needed game data either from files or from hard coded tables
            GameData.LoadGameData();

            _game = game;

            //Adds all the scenes in the game to the scene array 
            _scenes = new Scene[Enum.GetValues(typeof(SceneType)).Length];

            _scenes[(int)SceneType.MainMenu] = new MainMenuScene();
            _scenes[(int)SceneType.Game] = new GameScene();
            _scenes[(int)SceneType.Pause] = new PauseMenuScene();
            _scenes[(int)SceneType.GameOver] = new GameOverScene();
            ChangeScene(SceneType.MainMenu);

            //Changes the windows size
            _game.Graphics.PreferredBackBufferWidth = WindowWidth;
            _game.Graphics.PreferredBackBufferHeight = WindowHeight;
            _game.Graphics.ApplyChanges();
        }

        //Updates the current scene
        public static void UpdateCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Update(gameTime);
        }

        //Draws the current scene
        public static void DrawCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Draw(gameTime,_game.SpriteBatch);
        }

        //Changes the current scene to the given scene
        public static void ChangeScene(SceneType scene)
        {
            _scenes[(int)scene].EnterScene();
            _currentSceneType = scene;       
        }

        //Closes the game
        public static void ExitGame()
        {
            _game.Exit();
        }
    }
}
