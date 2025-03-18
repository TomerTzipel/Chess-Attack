using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public enum ItemType
    {
        Key, Potion, DamageToken, SpeedToken, AttackSpeedToken, MaxHealthToken
    }
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
            if (item == ItemType.MaxHealthToken) OnAddMaxHealthToken?.Invoke();
            Items[(int)item]++;

            OnInventoryChange?.Invoke(this);
        }

        public bool UseItem(ItemType item)
        {
            if (Items[(int)item] <= 0) return false;
            Items[(int)item]--;
            OnInventoryChange?.Invoke(this);
            return true;
        }
    }
}
