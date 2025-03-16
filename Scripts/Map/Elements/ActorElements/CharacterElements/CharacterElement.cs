using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Slay_The_Basilisk_MonoGame
{
    public struct CharacterElementStats
    {
        public int Damage;
        public int MaxHealth;
        public double MoveCooldown;
        public double AttackCooldown;
    }

    public abstract class CharacterElement : ActorElement, IMyUpdateable, IHitable
    {
        protected int _damage;
        protected double _moveCooldown;
        protected double _attackCooldown;

        protected bool _isOnCooldown;

        protected Timer _timer;
        protected Texture2D _cooldownSprite;

        public HealthHandler HealthHandler { get; private set; }

        public CharacterElement(Texture2D regularSprite, Texture2D cooldownSprite, Point mapPosition, CharacterElementStats stats) : base(regularSprite, mapPosition)
        {
            _cooldownSprite = cooldownSprite;
            _damage = stats.Damage;
            _moveCooldown = stats.MoveCooldown;
            _attackCooldown = stats.AttackCooldown;

            _timer = new Timer();
            _timer.OnTimerOver += HandleTimerOver;

            HealthHandler = new HealthHandler(stats.MaxHealth);
            HealthHandler.OnDeath += Die;

            _isOnCooldown = false;
        }

        //Move or Attack
        public abstract void Act(Direction direction);


        protected void Move(Direction direction)
        {
            RunManager.CurrentLevel.Map.MoveElementAtInDirection(_mapPosition, direction);
            _mapPosition.MovePointInDirection(direction);
            StartCooldown(_moveCooldown);
        }

        protected void Attack(IHitable hitable,Direction direction)
        {
            if (hitable.TakeDamage(_damage))
            {
                Move(direction);
            }

            StartCooldown(_attackCooldown);
        }

        protected void StartCooldown(double cooldown)
        {
            _timer.Start(cooldown);
            _isOnCooldown = true;
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

            spriteBatch.Draw(_cooldownSprite, WorldPosition, new Rectangle(0, 0, _cooldownSprite.Width, rectangleHeight), Color.White);
        }

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
    }
}
