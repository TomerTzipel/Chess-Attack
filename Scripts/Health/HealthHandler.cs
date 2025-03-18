﻿
namespace ChessOut.Health
{
    public class HealthHandler
    {
        private int _currentHealth;
        private int _maxHealth;

        public bool IsDead { get; private set; }

        public event EventHandler<HealthChangeEventArgs> OnHealthChanged;
        public event Action OnDeath;
        public HealthHandler(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            IsDead = false;
        }

        public void ModifyCurrentHealthByMaxHealthPercent(int percent)
        {
            if (percent == 0 || IsDead) return;

            int value = (int)((_maxHealth * percent) / 100f);

            ModifyHealth(value, HealthChangeMode.Current);
        }
        public void ModifyHealth(int value, HealthChangeMode mode)
        {
            if (value == 0 || IsDead) return;

            switch (mode)
            {
                case HealthChangeMode.Current:
                    ModifyCurrentHealth(value);
                    break;
                case HealthChangeMode.Max:
                    ModifyMaxHealth(value);
                    break;
            }
            if (_currentHealth <= 0) Die();
        }
        private void ModifyCurrentHealth(int value)
        {
            _currentHealth += value;

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
            }
            OnHealthChanged?.Invoke(this, new HealthChangeEventArgs { NewHealth = _currentHealth, MaxHealth = _maxHealth, mode = HealthChangeMode.Current });
        }

        private void ModifyMaxHealth(int value)
        {
            _maxHealth += value;

            if (_maxHealth < _currentHealth)
            {
                _currentHealth = _maxHealth;
            }

            if (_maxHealth < 0)
            {
                _maxHealth = 1; //So the hitable dies but the max health can't be 0 cause it will casue division by 0
                _currentHealth = 0;
            }

            OnHealthChanged?.Invoke(this, new HealthChangeEventArgs { NewHealth = _currentHealth, MaxHealth = _maxHealth, mode = HealthChangeMode.Max });
        }

        private void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}
