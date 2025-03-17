using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace Slay_The_Basilisk_MonoGame
{

    public enum Asset
    {
        Player,PlayerCD,
        Rook,RookCD, Bishop, BishopCD,Queen,QueenCD,
        Chest,Vase,
        Button
    }

    public enum TileVariant
    {
        Light,Dark
    }
    public static class AssetsManager
    {
        public static SpriteFont Font;
        public static Texture2D[] Stairs;

        private static Dictionary<Asset, Texture2D> Assets = new Dictionary<Asset, Texture2D>(8);
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
            Assets.Add(Asset.Button, content.Load<Texture2D>("UI/Button"));


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
