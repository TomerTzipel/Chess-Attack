using Microsoft.Xna.Framework;
using Slay_The_Basilisk_MonoGame.Scripts.Management.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public static class GameManager
    {
        public const int WindowWidth = 1920;
        public const int WindowHeight = 1080;

        public static Game1 Game;
        private static Scene[] _scenes;
        private static SceneType _currentSceneType;

        public static bool IsContinuingRun = false;

        public static void StartGame(Game1 game)
        {
            GameData.LoadGameData();
            Game = game;
            _scenes = new Scene[Enum.GetValues(typeof(SceneType)).Length];

            _scenes[(int)SceneType.MainMenu] = new MainMenuScene();
            _scenes[(int)SceneType.Game] = new GameScene();
            _scenes[(int)SceneType.Pause] = new PauseMenuScene();
            ChangeScene(SceneType.MainMenu);

            Game.Graphics.PreferredBackBufferWidth = WindowWidth;
            Game.Graphics.PreferredBackBufferHeight = WindowHeight;
            Game.Graphics.ApplyChanges();
        }

        public static void UpdateCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Update(gameTime);
        }

        public static void DrawCurrentScene(GameTime gameTime)
        {
            _scenes[(int)_currentSceneType].Draw(gameTime,Game.SpriteBatch);
        }

        public static void ChangeScene(SceneType scene)
        {
            _scenes[(int)scene].EnterScene();
            _currentSceneType = scene;       
        }

        public static void Exit()
        {
            Game.Exit();
        }
    }
}
