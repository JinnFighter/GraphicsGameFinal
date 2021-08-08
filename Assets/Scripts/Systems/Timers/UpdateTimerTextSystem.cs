using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateTimerTextSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TimerRef, TextRef, TimeChangeEvent> _filter = null;
        
        void IEcsRunSystem.Run() 
        {
            foreach(var index in _filter)
            {
                var timerRef = _filter.Get1(index);
                var timeChangeEvent = _filter.Get3(index);

                var entity = _filter.GetEntity(index);
                ref var updateTextEvent = ref entity.Get<UpdateTextEvent>();
                updateTextEvent.Text = timerRef.TimerFormat.GetFormattedTime(timeChangeEvent.CurrentTime);
            }
        }
    }
}