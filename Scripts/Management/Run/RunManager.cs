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
    public enum Difficulity
    {
        Easy,Normal,Hard
    }

    public static class RunManager
    {
        public static Level CurrentLevel { get; private set; }

        public static PlayerElement Player { get; private set; }
        private static HUD _hud;
        private static int _levelCount;
        private static int _numberOfLevels;

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
