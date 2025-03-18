using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public enum LootOrigin
    {
        Chest, Vase, Rook, Bishop, Queen, Level
    }
    public static class LootManager
    {
        public static void GivePlayerLootFromOrigin(LootOrigin origin)
        {
            Dictionary<ItemType, int> itemsChancesMap = GameData.LootTables[origin];
            int[] itemsChances = new int[itemsChancesMap.Count+1];
            int nothingPercent = 100;
            for (int i = 0; i < itemsChancesMap.Count; i++)
            {
                itemsChances[i] = itemsChancesMap[(ItemType)i];
                nothingPercent -= itemsChances[i];
            }

            itemsChances[itemsChancesMap.Count] = nothingPercent;

            try
            {
                int itemIndex = MathUtil.RandomWeightedIndex(itemsChances);

                if (itemIndex == itemsChancesMap.Count) return;

                RunManager.Player.Inventory.AddItem((ItemType)itemIndex);
            }
            catch
            {
                Debug.WriteLine("Detected loot percent not summing to 100!");
                return;
            }
            
        }
    }
}
