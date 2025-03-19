
namespace ChessOut.Loot
{
    //Encapsulates and array of items, allows to add/remove items and getting the current amount of an item
    //Uses the ItemType enum as an indexer
    public class Inventory
    {
        public int[] Items { get; private set; }

        public event Action OnAddMaxHealthToken;
        public event Action<Inventory> OnInventoryChange;
        public Inventory()
        {
            Items = new int[Enum.GetValues(typeof(ItemType)).Length];
        }
        public int GetItemAmount(ItemType item)
        {
            return Items[(int)item];
        }
        public void AddItem(ItemType item)
        {
            //We need this so player knows to raise Max HP on the Max Health Token pick up
            if (item == ItemType.MaxHealthToken) OnAddMaxHealthToken?.Invoke();

            Items[(int)item]++;

            OnInventoryChange?.Invoke(this);
        }

        public bool RemoveItem(ItemType item)
        {
            if (Items[(int)item] <= 0) return false;
            Items[(int)item]--;
            OnInventoryChange?.Invoke(this);
            return true;
        }
    }
}
