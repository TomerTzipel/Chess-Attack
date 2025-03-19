
namespace ChessOut.MapSystem.Elements
{
    //The player element, hold the inventory and stats
    public class PlayerElement : CharacterElement
    {
        public Inventory Inventory { get; private set; }

        //Calculates the stats in accordance with the inventory
        public override int Damage 
        {
            get 
            {
                int damage = base.Damage + (Inventory.Items[(int)ItemType.DamageToken] * GameData.PlayerDamageScaling);
                return damage;
            } 
        }
        public override double AttackCooldown
        {
            get
            {
                double lowerBound = GameData.PlayerCooldownLowerBound;
                double attackCooldown = base.AttackCooldown - (Inventory.Items[(int)ItemType.AttackSpeedToken] * GameData.PlayerAttackSpeedScaling);
                return attackCooldown < lowerBound ? lowerBound : attackCooldown;
            }
        }
        public override double MoveCooldown
        {
            get
            {
                double lowerBound = GameData.PlayerCooldownLowerBound;
                double moveCooldown = base.MoveCooldown - (Inventory.Items[(int)ItemType.SpeedToken] * GameData.PlayerMoveSpeedScaling);
                return moveCooldown < lowerBound ? lowerBound : moveCooldown;
            } 
        }
        
        public Point MapPosition
        {
            get { return _mapPosition; }
            set { _mapPosition = value; }
        }

        public PlayerElement() : base(AssetsManager.GetAsset(Asset.Player), AssetsManager.GetAsset(Asset.PlayerCD), new Point(), GameData.PlayerStats) 
        {
            Inventory = new Inventory();
            Inventory.OnAddMaxHealthToken += AddMaxHealth; 

            //Starts the timer so it will be full when the game start so the player could move immidiatly  
            _timer.Start(0.001d);
        }
        public bool UseKey()
        {
            return Inventory.RemoveItem(ItemType.Key);
        }
        public void DrinkHealthPotion()
        {
            if (Inventory.RemoveItem(ItemType.Potion))
            {
                HealthHandler.ModifyCurrentHealthByMaxHealthPercent(25);
            }     
        }

        //Acts in the direction given from the player input
        public override void Act(Direction direction)
        {
            if (_isOnCooldown) return;

            Point targetPosition = new Point(_mapPosition).MovePointInDirection(direction);
            MapElement elementInDirection = RunManager.CurrentLevel.Map.ElementAt(targetPosition);

            //Can't move to outside the map or to obstacles
            if (elementInDirection == null || elementInDirection is IObstacle) return;

            //Moves onto an empty element
            if (elementInDirection is EmptyElement)
            {
                Move(direction);
                return;
            }
            //Attacks a hitable elemnt
            if(elementInDirection is IHitable hitable)
            {
                Attack(hitable,direction);
            }
        }

        public override void Die()
        {
            RunManager.EndRun(false);
        }
        private void AddMaxHealth()
        {
            HealthHandler.ModifyHealth(50, HealthChangeMode.Max);
        }
    }
}
