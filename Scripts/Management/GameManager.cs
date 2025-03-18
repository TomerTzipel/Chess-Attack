
namespace ChessOut
{
    public static class GameManager
    {
        public const int WindowWidth = 1920;
        public const int WindowHeight = 1080;

        public static bool IsContinuingRun = false;

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

            _game.Graphics.PreferredBackBufferWidth = WindowWidth;
            _game.Graphics.PreferredBackBufferHeight = WindowHeight;
            _game.Graphics.ApplyChanges();
        }

        public static void UpdateCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Update(gameTime);
        }

        public static void DrawCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Draw(gameTime,_game.SpriteBatch);
        }

        public static void ChangeScene(SceneType scene)
        {
            _scenes[(int)scene].EnterScene();
            _currentSceneType = scene;       
        }

        public static void Exit()
        {
            _game.Exit();
        }
    }
}
