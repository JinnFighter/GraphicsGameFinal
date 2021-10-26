using Leopotam.Ecs;
using Pixelgrid.Components.Time.Timer;
using Pixelgrid.UI.Models;
using Pixelgrid.UI.Presenters;
using Pixelgrid.UI.Views;

namespace Pixelgrid.Systems.Timers {
    public sealed class GenerateCountdownTimersSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world = null;
        private readonly TimersContainer _timersContainer = null;

        public void Init()
        {
            var timerEntity = _world.NewEntity();
            ref var timer = ref timerEntity.Get<Timer>();
            timerEntity.Get<CountdownTimer>();
            
            var timerModel = new TimerModel();
            timer.TimeChangeEvent += timerModel.UpdateTime;

            var presenter = new TimerPresenter(timerModel, _timersContainer.CountdownTimer.GetComponent<TimerView>());
        }
    }
}