


namespace ChessOut.MapSystem.Elements
{
   //An element that is capable of moving and attacking, meaning enemies and the player.
   //Works by having an inner timer to know when an action is availble
   //An acion is either moving or attacking
    public abstract class CharacterElement : ActorElement, IMyUpdateable, IHitable
    {
        protected int _baseDamage;
        protected double _attackCooldown;
        protected double _moveCooldown;
      
        protected bool _isOnCooldown;

        protected Timer _timer;
        protected Texture2D _cooldownSprite;

        public HealthHandler HealthHandler { get; private set; }

        //The properties are virtual so the player can buff itself using the items they collect
        public virtual int Damage { get { return _baseDamage; } protected set { _baseDamage = value; } }
        public virtual double AttackCooldown { get { return _attackCooldown; } protected set { _attackCooldown = value; } }
        public virtual double MoveCooldown { get { return _moveCooldown; } protected set { _moveCooldown = value; } }
     
        public CharacterElement(Texture2D regularSprite, Texture2D cooldownSprite, Point mapPosition, CharacterElementStats stats) : base(regularSprite, mapPosition)
        {
            _cooldownSprite = cooldownSprite;
            _baseDamage = stats.Damage;
            _attackCooldown = stats.AttackCooldown;
            _moveCooldown = stats.MoveCooldown;
            
            _timer = new Timer();
            _timer.OnTimerOver += HandleTimerOver;

            HealthHandler = new HealthHandler(stats.MaxHealth);
            HealthHandler.OnDeath += Die;

            _isOnCooldown = false;
        }
        public virtual void Update(GameTime gameTime)
        {
            if (_timer.IsActive)
            {
                _timer.Tick(gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            int rectangleHeight = (int)((double)_cooldownSprite.Height * _timer.CompletionPercent);
            int Y = (int)((double)ElementSize * _timer.CompletionPercent);
            Y = ElementSize - Y;
            spriteBatch.Draw(_cooldownSprite, WorldPosition + new Vector2(0, Y), new Rectangle(0, Y, _cooldownSprite.Width, rectangleHeight), Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }
        //Move or Attack
        public abstract void Act(Direction direction);
        private void HandleTimerOver()
        {
            _isOnCooldown = false;
        }

        public bool TakeDamage(int damage)
        {
            HealthHandler.ModifyHealth(-damage, HealthChangeMode.Current);
            return HealthHandler.IsDead;
        }

        public abstract void Die();

        protected void Move(Direction direction)
        {
            RunManager.CurrentLevel.Map.MoveElementAtInDirection(_mapPosition, direction);
            _mapPosition.MovePointInDirection(direction);
            StartCooldown(MoveCooldown);
        }

        protected void Attack(IHitable hitable,Direction direction)
        {
            if (hitable.TakeDamage(Damage))
            {
                Move(direction);
            }

            StartCooldown(AttackCooldown);
        }

        protected void StartCooldown(double cooldown)
        {
            _timer.Start(cooldown);
            _isOnCooldown = true;
        }

       

       
    }
}
