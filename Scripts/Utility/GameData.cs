using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


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
        //Defaults in case the file wasn't found
        public static CharacterElementStats PlayerStats = new CharacterElementStats { Damage = 10, MaxHealth = 100, AttackCooldown = 1d, MoveCooldown = 0.3d };
        public static CharacterElementStats RookStats = new CharacterElementStats { Damage = 5, MaxHealth = 50, AttackCooldown = 1d, MoveCooldown = 1d };
        public static Camera Camera = new Camera { Width = 30, Height = 16, WorldPosition = Vector2.Zero };
        public const int SectionSize = 3;
        public static List<MapData> Maps = new List<MapData>();

        //Map sizes and composition per level
        public static void LoadGameData()
        {
            //Default Maps
            Maps.Add(new MapData { Size = 3, SectionsToGenerate = 6, EnemySectionsToGenerate = 1, ChestSectionsToGenerate = 1 });
            Maps.Add(new MapData { Size = 5, SectionsToGenerate = 15,EnemySectionsToGenerate = 3, ChestSectionsToGenerate = 1 });
            Maps.Add(new MapData { Size = 5, SectionsToGenerate = 19, EnemySectionsToGenerate = 5, ChestSectionsToGenerate = 2 });
            Maps.Add(new MapData { Size = 6, SectionsToGenerate = 22, EnemySectionsToGenerate = 7, ChestSectionsToGenerate = 3 });
            Maps.Add(new MapData { Size = 6, SectionsToGenerate = 27, EnemySectionsToGenerate = 9, ChestSectionsToGenerate = 4 });
            Maps.Add(new MapData { Size = 7, SectionsToGenerate = 34, EnemySectionsToGenerate = 11, ChestSectionsToGenerate = 5 });
        }

        public static MapData GetMapByLevel(int levelIndex)
        {
            if(levelIndex >= Maps.Count) throw new IndexOutOfRangeException();

            return Maps[levelIndex];
        }
    }
}
