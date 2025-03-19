
namespace ChessOut.Utility
{
    //A simple timer that check if the timer is up with deltaTime and its duration
    public class Timer
    {
        private double _currentTime;
        private double _duration; 
        public bool IsActive { get; private set; } = false;

        //Returns how much of the duration was completed
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

        //Starts the timer
        public void Start(double duration)
        {
            IsActive = true;
            _currentTime = 0d;
            _duration = duration;
        }

        //Adds delta time to the timer
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
