using Pixelgrid.UI.Models;
using Pixelgrid.UI.Views;

namespace Pixelgrid.UI.Presenters
{
    public class TimerPresenter
    {
        private readonly ITimerView _timerView;
        public TimerPresenter(TimerModel timerModel, ITimerView timerView)
        {
            timerModel.TimeChangedEvent += UpdateTime;
            _timerView = timerView;
        }

        private void UpdateTime(float currentTime) => _timerView.UpdateTime(currentTime);
    }
}
