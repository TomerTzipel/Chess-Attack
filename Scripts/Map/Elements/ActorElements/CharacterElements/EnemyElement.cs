

namespace ChessOut.MapSystem.Elements.Enemies
{
    //Every enemy that inherits only needs to calculate the directions it can move to reach the player
    //and what it does when dying
    public abstract class EnemyElement : CharacterElement
    {
        //A static list of all created enemies so the level can check if it can be exited
        public static List<EnemyElement> _enemies = new List<EnemyElement>(8);

        public static void ClearEnemies()
        {
            _enemies.Clear();
        }

        protected float _aggroRange;

        public EnemyElement(Texture2D regularSprite, Texture2D cooldownSprite, Point mapPosition,float aggroRange, CharacterElementStats stats) : base(regularSprite, cooldownSprite, mapPosition, stats)
        {
            _aggroRange = aggroRange;
            _enemies.Add(this);
            _timer.OnTimerOver += AttemptToAct;
        }

        public override void Update(GameTime gameTime)
        {
            if (_timer.IsActive)
            {
                _timer.Tick(gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public void StartAI()
        {
            AttemptToAct();
        }
        public override void Die()
        {
            _enemies.Remove(this);
        }
        public override void Act(Direction direction)
        {
            Point targetPosition = new Point(_mapPosition).MovePointInDirection(direction);
            MapElement elementInDirection = RunManager.CurrentLevel.Map.ElementAt(targetPosition);

            if (elementInDirection == null || elementInDirection is IObstacle) return;

            if (elementInDirection is EmptyElement)
            {
                Move(direction);
                return;
            }
            if (elementInDirection is PlayerElement player)
            {
                Attack(player, direction);
            }
        }

       

        private void AttemptToAct()
        {
            List<Direction> availableDirections = CalculateAvailableDirections();

            while (!_isOnCooldown && availableDirections.Count > 0)
            {
                int chosenIndex = MathUtil.RandomIndex(availableDirections.Count);
                Direction chosenDirection = availableDirections[chosenIndex];
                availableDirections.Remove(chosenDirection);
                Act(chosenDirection);
            }

            if (!_isOnCooldown)
            {
                StartCooldown(_moveCooldown);
            }
        }

       

        protected abstract List<Direction> CalculateAvailableDirections();
        protected abstract void CalculateDirectionsToPlayer(List<Direction> directions);
    }
}
