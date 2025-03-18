
namespace ChessOut.Run
{
    public static class RunManager
    {
        private static HUD _hud;
        private static int _levelCount;
        private static int _numberOfLevels;
        public static bool LastRunResult { get; private set; }
        public static Level CurrentLevel { get; private set; }
        public static PlayerElement Player { get; private set; }
        public static bool IsRunActive { get; set; } = false;

        public static Point PlayerPosition { get { return Player.MapPosition; } }
        public static Difficulity CurrentDifficulity 
        { 
            get 
            {
                int completionPercent = MathUtil.GetPercent(_levelCount, _numberOfLevels);
                if(completionPercent < 30) return Difficulity.Easy;
                if(completionPercent < 70) return Difficulity.Normal;
                return Difficulity.Hard;
            } 
        }

        public static void StartRun(int numberOfLevels)
        {
            IsRunActive = true;

            Player = new PlayerElement();

            _levelCount = 0;
            _numberOfLevels = numberOfLevels;

            _hud = new HUD(Player);

            StartLevel(0);
        }

        public static void NextLevel()
        {
            _levelCount++;
            if(_levelCount >= _numberOfLevels)
            {
                EndRun(true);
                return;
            }

            StartLevel(_levelCount);
        }

        public static void EndRun(bool runResult)
        {
            IsRunActive = false;
            LastRunResult = runResult;
            GameManager.ChangeScene(SceneType.GameOver);
        }

        public static void Update(GameTime gameTime)
        {
            CurrentLevel.Update(gameTime);
        }
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(gameTime, spriteBatch);
            _hud.Draw(gameTime, spriteBatch);
        }
        public static void HandlePlayerInput(object sender, EventArgs args)
        {
            Player.Act(((MovementEventArgs)args).Direction);
        }

        private static void StartLevel(int levelIndex)
        {
            CurrentLevel = new Level(Player, levelIndex);
            foreach (var enemy in EnemyElement._enemies)
            {
                enemy.StartAI();
            }
        }
    }
}
