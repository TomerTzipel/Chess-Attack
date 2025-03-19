
namespace ChessOut.Scenes
{
    //The scene of the game, managed by the Run Manager
    //Handles player keyboard input, as it happens to be the only non menu scene.
    public class GameScene : Scene
    {
        public GameScene() 
        {
            //Mapping player controls to the input map.
            _inputMap.AddAction(new KeyboardAction(Keys.W, new MovementEventArgs { Direction = Direction.Up })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.S, new MovementEventArgs { Direction = Direction.Down })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.A, new MovementEventArgs { Direction = Direction.Left })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.D, new MovementEventArgs { Direction = Direction.Right })).Action += RunManager.HandlePlayerInput;
            _inputMap.AddAction(new KeyboardAction(Keys.Escape)).Action += OnPause;
            _inputMap.AddAction(new KeyboardAction(Keys.Q)).Action += DrinkPotion;
        }

        public override void EnterScene()
        {
            //Checks if a new run needs to be created, or continue an existing one
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
