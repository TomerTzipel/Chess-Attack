using System.Diagnostics;

namespace ChessOut.Loot
{
    //A static class to allow multiple loot origins give the player an item from their loot table
    //Loot table sits in GameData and the loot is randomized using percentages per item.
    public static class LootManager
    {
        public static void GivePlayerLootFromOrigin(LootOrigin origin)
        {
            //Pulling the weights according to the origin form the loot table
            Dictionary<ItemType, int> itemsChancesMap = GameData.LootTables[origin];

            //Creating an array containing the chance for each item, and adding an additional slot for getting nothing
            //Nothing chance is 100-Sum(itemChance)
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
                //Getting a random index according to the percents in the array
                int itemIndex = MathUtil.RandomIndexFromPercentArray(itemsChances);

                //Nothing was rolled
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
