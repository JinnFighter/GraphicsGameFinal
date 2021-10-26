namespace Pixelgrid.UI.Models
{
    public class TimerModel
    {
        private float _currentTime;

        public delegate void TimeChanged(float currentTime);

        public event TimeChanged TimeChangedEvent;

        public TimerModel()
        {
            _currentTime = 0f;
        }

        public void UpdateTime(float time)
        {
            _currentTime = time;
            TimeChangedEvent?.Invoke(_currentTime);
        }
    }
}
