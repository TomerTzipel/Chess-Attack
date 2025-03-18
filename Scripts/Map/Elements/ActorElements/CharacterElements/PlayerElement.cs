using System.Diagnostics;

namespace Slay_The_Basilisk_MonoGame
{


    public class PlayerElement : CharacterElement
    {
        public Inventory Inventory { get; private set; }
        public override int Damage 
        {
            get 
            {
                int damage = base.Damage + (Inventory.Items[(int)ItemType.DamageToken] * GameData.PlayerDamageScaling);
                Debug.WriteLine(damage);
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

            _timer.Start(0.001d);
        }
        public bool UseKey()
        {
            return Inventory.UseItem(ItemType.Key);
        }
        public void DrinkHealthPotion()
        {
            if (Inventory.UseItem(ItemType.Potion))
            {
                HealthHandler.ModifyCurrentHealthByMaxHealthPercent(25);
            }     
        }

        private void AddMaxHealth()
        {
            HealthHandler.ModifyHealth(50, HealthChangeMode.Max);
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
