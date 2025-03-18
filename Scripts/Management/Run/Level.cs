using Microsoft.Xna.Framework;

namespace ChessOut.Run
{
   
    public class Level : IMyDrawable, IMyUpdateable
    {
        public Camera Camera { get; private set; }
        public Map Map { get; private set; }
        public int LevelIndex { get; private set; }

        public Level(PlayerElement player,int levelIndex)
        {
            //Clear enemies between levels to avoid having enemies persist between levels
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
