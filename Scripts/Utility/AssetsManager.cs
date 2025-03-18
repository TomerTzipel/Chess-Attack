using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace ChessOut.Utility
{

    //A class that hold all of the game's assets
    //Loads them from content right at the start of the game
    public static class AssetsManager
    {
        public static SpriteFont Font;
        public static Texture2D[] Stairs;

        //The dictionary all the assets are pulled from
        private static Dictionary<Asset, Texture2D> Assets = new Dictionary<Asset, Texture2D>(8);

        //Due to having both light and dark variants of the tiles and staires we pull them from
        //array/matrix instead accoring to the enum that was chosen for the tile
        private static Texture2D[,] Tiles;
        public static void LoadContent(ContentManager content)
        {
            Assets.Add(Asset.Player, content.Load<Texture2D>("Elements/King"));
            Assets.Add(Asset.PlayerCD, content.Load<Texture2D>("Elements/KingCD"));
            Assets.Add(Asset.Rook, content.Load<Texture2D>("Elements/Rook"));
            Assets.Add(Asset.RookCD, content.Load<Texture2D>("Elements/RookCD"));
            Assets.Add(Asset.Bishop, content.Load<Texture2D>("Elements/Bishop"));
            Assets.Add(Asset.BishopCD, content.Load<Texture2D>("Elements/BishopCD"));
            Assets.Add(Asset.Queen, content.Load<Texture2D>("Elements/Queen"));
            Assets.Add(Asset.QueenCD, content.Load<Texture2D>("Elements/QueenCD"));
            Assets.Add(Asset.Chest, content.Load<Texture2D>("Elements/Chest"));
            Assets.Add(Asset.Vase, content.Load<Texture2D>("Elements/Vase"));
            Assets.Add(Asset.Button, content.Load<Texture2D>("UI/Button"));
            Assets.Add(Asset.Panel, content.Load<Texture2D>("UI/Panel"));
            Assets.Add(Asset.HpBar, content.Load<Texture2D>("UI/HpBar"));
            Assets.Add(Asset.HpBarFill, content.Load<Texture2D>("UI/HpBarFill"));
            Assets.Add(Asset.KeyIcon, content.Load<Texture2D>("UI/KeyIcon"));
            Assets.Add(Asset.PotionIcon, content.Load<Texture2D>("UI/PotionIcon"));
            Assets.Add(Asset.DamageIcon, content.Load<Texture2D>("UI/DamageIcon"));
            Assets.Add(Asset.AttackSpeedIcon, content.Load<Texture2D>("UI/AttackSpeedIcon"));
            Assets.Add(Asset.SpeedIcon, content.Load<Texture2D>("UI/SpeedIcon"));
            Font = content.Load<SpriteFont>("Fonts/Font");

            Tiles = new Texture2D[2,7];
            Stairs = new Texture2D[2];

            for (int i = 0; i < 7; i++)
            {
                Tiles[(int)TileVariant.Light,i] = content.Load<Texture2D>($"Tiles/LightTile{i+1}");
                Tiles[(int)TileVariant.Dark, i] = content.Load<Texture2D>($"Tiles/Tile{i + 1}");
            }
            Stairs[(int)TileVariant.Light] = content.Load<Texture2D>($"Tiles/LightStairs");
            Stairs[(int)TileVariant.Dark] = content.Load<Texture2D>($"Tiles/Stairs");
        }

        public static Texture2D GetAsset(Asset asset)
        {
            return Assets[asset];
        }

        public static Texture2D RandomTileByVariant(TileVariant variant) 
        {
            int chosenIndex = MathUtil.RandomIndex(Tiles.GetLength(1));
            return Tiles[(int)variant, chosenIndex];
        }

    }
}
