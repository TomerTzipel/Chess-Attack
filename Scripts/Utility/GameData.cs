using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace Slay_The_Basilisk_MonoGame
{
    //It's a class and not a struct so I could return null
    public class MapData
    {
        public int Size;
        public int SectionsToGenerate;
        public int EnemySectionsToGenerate;
        public int ChestSectionsToGenerate;
    }

    public static class GameData
    {
        private const string CHARACTER_DATA_PATH = "..\\..\\..\\Data\\Characters_Data.txt";
        private const string MAP_DATA_PATH = "..\\..\\..\\Data\\Map_Data.txt";
        //Defaults in case the file wasn't found
        public static CharacterElementStats PlayerStats = new CharacterElementStats { MaxHealth = 200, Damage = 25, AttackCooldown = 1d, MoveCooldown = 0.4d };
        public static CharacterElementStats RookStats = new CharacterElementStats { MaxHealth = 50, Damage = 20, AttackCooldown = 1d, MoveCooldown = 1d };
        public static CharacterElementStats BishopStats = new CharacterElementStats { MaxHealth = 50, Damage = 20, AttackCooldown = 1d, MoveCooldown = 1d };
        public static CharacterElementStats QueenStats = new CharacterElementStats { MaxHealth = 50, Damage = 20, AttackCooldown = 1d, MoveCooldown = 1d };
        public static int RookRange = 5;
        public static int BishopRange = 5;
        public static int QueenRange = 5;
        public static Camera Camera = new Camera { Width = 30, Height = 16, WorldPosition = Vector2.Zero };
        public const int SectionSize = 3;
        public static List<MapData> Maps = new List<MapData>();
        public const int PlayerDamageScaling = 10;
        public const double PlayerAttackSpeedScaling = 0.05d;
        public const double PlayerMoveSpeedScaling = 0.01d;
        public const double PlayerCooldownLowerBound = 0.2d;

        public static Dictionary<LootOrigin, Dictionary<ItemType, int>> LootTables;
        private static int[,] lootData;

        public static void LoadGameData()
        {
            try
            {
                string rawData = File.ReadAllText(CHARACTER_DATA_PATH);
                string[] charactersRawData = rawData.Replace("\r\n", "").Split(',');
                Stack<string[]> charactersData = new Stack<string[]>(charactersRawData.Length);

                foreach (string characterRawData in charactersRawData)
                {
                    charactersData.Push(characterRawData.Split(":"));
                }

                while (charactersData.Count > 0)
                {
                    string[] characterData = charactersData.Pop();
                    AddCharacterData(characterData);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }

            //Load Map Data from file - This file doesn't actually exist to demonstrate the error handling and the back up case
            try
            {
                string rawData = File.ReadAllText(MAP_DATA_PATH);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

                //Load default maps instead Default Maps
                Maps.Add(new MapData { Size = 3, SectionsToGenerate = 6, EnemySectionsToGenerate = 1, ChestSectionsToGenerate = 1 });
                Maps.Add(new MapData { Size = 5, SectionsToGenerate = 15, EnemySectionsToGenerate = 3, ChestSectionsToGenerate = 1 });
                Maps.Add(new MapData { Size = 5, SectionsToGenerate = 19, EnemySectionsToGenerate = 5, ChestSectionsToGenerate = 2 });
                Maps.Add(new MapData { Size = 6, SectionsToGenerate = 22, EnemySectionsToGenerate = 7, ChestSectionsToGenerate = 3 });
                Maps.Add(new MapData { Size = 6, SectionsToGenerate = 27, EnemySectionsToGenerate = 9, ChestSectionsToGenerate = 4 });
                Maps.Add(new MapData { Size = 7, SectionsToGenerate = 34, EnemySectionsToGenerate = 11, ChestSectionsToGenerate = 5 });
            }

            SetUpLootTables();
        }

        public static MapData GetMapByLevel(int levelIndex)
        {
            if(levelIndex >= Maps.Count) throw new IndexOutOfRangeException();

            return Maps[levelIndex];
        }
        private static void InitializeLootData(int originCount,int itemCount)
        {
            lootData = new int[originCount, itemCount];
            lootData[(int)LootOrigin.Chest,(int)ItemType.Key] = 0; lootData[(int)LootOrigin.Chest, (int)ItemType.Potion] = 50;
            lootData[(int)LootOrigin.Chest, (int)ItemType.MaxHealthToken] = 0; lootData[(int)LootOrigin.Chest,(int)ItemType.DamageToken] = 20;
            lootData[(int)LootOrigin.Chest, (int)ItemType.AttackSpeedToken] = 10; lootData[(int)LootOrigin.Chest, (int)ItemType.SpeedToken] = 20;

            lootData[(int)LootOrigin.Vase, (int)ItemType.Key] = 10; lootData[(int)LootOrigin.Vase, (int)ItemType.Potion] = 10;
            lootData[(int)LootOrigin.Vase, (int)ItemType.MaxHealthToken] = 0; lootData[(int)LootOrigin.Vase, (int)ItemType.DamageToken] = 0;
            lootData[(int)LootOrigin.Vase, (int)ItemType.AttackSpeedToken] = 0; lootData[(int)LootOrigin.Vase, (int)ItemType.SpeedToken] = 0;

            lootData[(int)LootOrigin.Rook, (int)ItemType.Key] = 0; lootData[(int)LootOrigin.Rook, (int)ItemType.Potion] = 30;
            lootData[(int)LootOrigin.Rook, (int)ItemType.MaxHealthToken] = 0; lootData[(int)LootOrigin.Rook, (int)ItemType.DamageToken] = 20;
            lootData[(int)LootOrigin.Rook, (int)ItemType.AttackSpeedToken] = 0; lootData[(int)LootOrigin.Rook, (int)ItemType.SpeedToken] = 0;

            lootData[(int)LootOrigin.Bishop, (int)ItemType.Key] = 30; lootData[(int)LootOrigin.Bishop, (int)ItemType.Potion] = 0;
            lootData[(int)LootOrigin.Bishop, (int)ItemType.MaxHealthToken] = 0; lootData[(int)LootOrigin.Bishop, (int)ItemType.DamageToken] = 0;
            lootData[(int)LootOrigin.Bishop, (int)ItemType.AttackSpeedToken] = 20; lootData[(int)LootOrigin.Bishop, (int)ItemType.SpeedToken] = 0;

            lootData[(int)LootOrigin.Queen, (int)ItemType.Key] = 0; lootData[(int)LootOrigin.Queen, (int)ItemType.Potion] = 0;
            lootData[(int)LootOrigin.Queen, (int)ItemType.MaxHealthToken] = 0; lootData[(int)LootOrigin.Queen, (int)ItemType.DamageToken] = 40;
            lootData[(int)LootOrigin.Queen, (int)ItemType.AttackSpeedToken] = 30; lootData[(int)LootOrigin.Queen, (int)ItemType.SpeedToken] = 30;

            lootData[(int)LootOrigin.Level, (int)ItemType.Key] = 0; lootData[(int)LootOrigin.Level, (int)ItemType.Potion] = 0;
            lootData[(int)LootOrigin.Level, (int)ItemType.MaxHealthToken] = 100; lootData[(int)LootOrigin.Level, (int)ItemType.DamageToken] = 0;
            lootData[(int)LootOrigin.Level, (int)ItemType.AttackSpeedToken] = 0; lootData[(int)LootOrigin.Level, (int)ItemType.SpeedToken] = 0;
        }
        private static void SetUpLootTables()
        {
            LootOrigin[] origins = (LootOrigin[])Enum.GetValues(typeof(LootOrigin));
            ItemType[] items = (ItemType[])Enum.GetValues(typeof(ItemType));

            InitializeLootData(origins.Length, items.Length);

            LootTables = new Dictionary<LootOrigin, Dictionary<ItemType, int>>(origins.Length);
            foreach (var origin in origins)
            {
                LootTables.Add(origin, new Dictionary<ItemType, int>(items.Length));
                foreach (var item in items)
                {
                    LootTables[origin].Add(item, lootData[(int)origin, (int)item]);
                }
            }
        }

        private static void AddCharacterData(string[] data)
        {
            CharacterElementStats stats = new CharacterElementStats 
            { 
                MaxHealth = int.Parse(data[1]) , 
                Damage = int.Parse(data[2]), 
                AttackCooldown = double.Parse(data[3]),
                MoveCooldown = double.Parse(data[4])
            };
            switch (data[0])
            {
                case "K":
                    PlayerStats = stats;
                    break;
                case "R":
                    RookStats = stats;
                    RookRange = int.Parse(data[5]);
                    break;
                case "B":
                    BishopStats = stats;
                    BishopRange = int.Parse(data[5]);
                    break;
                case "Q":
                    QueenStats = stats;
                    QueenRange = int.Parse(data[5]);
                    break;
                default:
                    break;
            }
        }
    }
}
