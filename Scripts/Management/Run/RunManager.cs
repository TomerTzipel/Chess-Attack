using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public static class RunManager
    {
        public static Level CurrentLevel { get; private set; }

        public static PlayerElement Player;
        private static int _levelCount;
        private static int _numberOfLevels;

        public static bool IsRunActive { get; set; } = false;
        public static Point PlayerPosition { get { return Player.MapPosition; } }
        public static void StartRun(int numberOfLevels)
        {
            IsRunActive = true;

            //MOVE THE NUMBERS TO A STATIC GAME SETTINGS
            Player = new PlayerElement();

            _levelCount = 0;
            _numberOfLevels = numberOfLevels;

            //with first level data
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

            //with next level data
            StartLevel(_levelCount);
        }

        public static void EndRun(bool runResult)
        {
            IsRunActive = false;
            if (runResult)
            {
                GameManager.ChangeScene(SceneType.MainMenu);
            }
            else
            {
                GameManager.ChangeScene(SceneType.MainMenu);
            }
        }

        public static void Update(GameTime gameTime)
        {
            CurrentLevel.Update(gameTime);
        }
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(gameTime, spriteBatch);
            //Draw HUD
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
