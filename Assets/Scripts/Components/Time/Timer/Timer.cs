namespace Pixelgrid.Components.Time.Timer 
{
    public struct Timer
    {
        public float startTime;
        private float _currentTime;
        public delegate void TimeChange(float time);

        public event TimeChange TimeChangeEvent;
        public float currentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                TimeChangeEvent?.Invoke(_currentTime);
            }
        }
    }
}