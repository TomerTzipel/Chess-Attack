using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace Slay_The_Basilisk_MonoGame
{
    public struct Camera
    {
        public int Width;
        public int Height;
        public Vector2 WorldPosition;
        public Point Origin { get { return RunManager.PlayerPosition - new Point(Width / 2, Height / 2); } }
    }
    public class Level : IMyDrawable, IMyUpdateable
    {
        public Camera Camera { get; private set; }
        public Map Map { get; private set; }
        public int LevelIndex { get; private set; }

        public Level(PlayerElement player,int levelIndex)
        {
            EnemyElement.ClearEnemies();

            Camera = GameData.Camera;

            MapData mapData;
            try
            {
                mapData = GameData.GetMapByLevel(levelIndex);
                Map = new Map(mapData);
            }
            catch 
            { 
                //Will never happen due to RunManager, but it could be an error if someone were to fuck up the code
                mapData = GameData.GetMapByLevel(0);
                Map = new Map(mapData);
            }

            LevelIndex = levelIndex;

            
        }
        public void Update(GameTime gameTime)
        {
            foreach (var enemy in EnemyElement._enemies) 
            {
                enemy.Update(gameTime);
            }
            RunManager.Player.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch);
        }
    }
}
