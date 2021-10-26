using Pixelgrid.UI.Models;

namespace Pixelgrid.UI.Presenters
{
    public class TimerPresenter
    {
        public TimerPresenter(TimerModel timerModel)
        {
            timerModel.TimeChangedEvent += UpdateTime;
        }

        private void UpdateTime(float currentTime)
        {
            
        }
    }
}
