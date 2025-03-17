using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public class Timer
    {
        private double _currentTime;
        private double _duration; 
        public bool IsActive { get; private set; } = false;
        public double CompletionPercent 
        { 
            get 
            {
                double value;
                value = (_currentTime / _duration > 1d) ? 1d : _currentTime / _duration;
                return value;
            } 
        }

        public event Action OnTimerOver;

        public Timer() { }

        public void Start(double duration)
        {
            IsActive = true;
            _currentTime = 0d;
            _duration = duration;
        }

        public void Tick(double deltaTime) 
        {
            _currentTime += deltaTime;
            if(_currentTime >= _duration)
            {
                IsActive = false;
                _currentTime = _duration;
                OnTimerOver?.Invoke();            
            }
        }
    }
}
