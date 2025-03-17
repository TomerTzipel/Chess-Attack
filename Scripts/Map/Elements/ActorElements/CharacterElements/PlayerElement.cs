namespace Slay_The_Basilisk_MonoGame
{
    public enum ItemType
    {
        Key,HealthPotion
    }
    public struct Inventory
    {
        public int Keys;
        public int HealthPotions;

        public void AddItem(ItemType item)
        {
            switch (item)
            {
                case ItemType.Key:
                    Keys++;
                    break;
                case ItemType.HealthPotion:
                    HealthPotions++;
                    break;
            }
        }
        //Inventory holder is in charge of checking if the item is at 0 before calling useItem
        public void UseItem(ItemType item)
        {
            switch (item)
            {
                case ItemType.Key:
                    Keys--;
                    break;
                case ItemType.HealthPotion:
                    HealthPotions--;
                    break;
            }
        }
    }

    public class PlayerElement : CharacterElement
    {
        public Inventory Inventory { get; private set; }
        public Point MapPosition
        {
            get { return _mapPosition; }
            set { _mapPosition = value; }
        }

        public PlayerElement() : base(AssetsManager.GetAsset(Asset.Player), AssetsManager.GetAsset(Asset.PlayerCD), new Point(), GameData.PlayerStats) 
        {
            Inventory = new Inventory { Keys = 0, HealthPotions = 0 };
            _timer.Start(0.001d);
        }
        public bool UseKey()
        {
            if(Inventory.Keys <= 0) return false;
            Inventory.UseItem(ItemType.Key);
            return true;
        }
        public void DrinkHealthPotion()
        {
            if (Inventory.HealthPotions <= 0) return;
            Inventory.UseItem(ItemType.HealthPotion);
            HealthHandler.ModifyCurrentHealthByMaxHealthPercent(25);
        }

        public override void Act(Direction direction)
        {
            if (_isOnCooldown) return;

            Point targetPosition = new Point(_mapPosition).MovePointInDirection(direction);
            MapElement elementInDirection = RunManager.CurrentLevel.Map.ElementAt(targetPosition);

            if (elementInDirection == null || elementInDirection is IObstacle) return;

            if (elementInDirection is EmptyElement)
            {
                Move(direction);
                return;
            }

            if(elementInDirection is IHitable hitable)
            {
                Attack(hitable,direction);
            }
        }

        public override void Die()
        {
            RunManager.EndRun(false);
        }
    }
}
