using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CheckBezierAnswerSystem : IEcsRunSystem 
    {
        private EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter;
        private EcsFilter<GameModeData, BezierLineData> _gameModeDataFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        void IEcsRunSystem.Run()
        {
            ref var lineDataComponent = ref _gameModeDataFilter.Get2(0);
            var lineDatas = lineDataComponent.Points;
            var eventReceiver = _eventReceiverFilter.GetEntity(0);
            foreach (var pixelIndex in _pixelsClickedFilter)
            {
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if (lineDataComponent.CurrentPoint >= lineDatas.Count)
                    eventReceiver.Get<GameOverEvent>();
                else
                {
                    if(position.Equals(lineDatas[lineDataComponent.CurrentPoint]))
                    {
                        eventReceiver.Get<CorrectAnswerEvent>();
                        lineDataComponent.CurrentPoint++;
                        if (lineDataComponent.CurrentPoint >= lineDatas.Count)
                            eventReceiver.Get<GameOverEvent>();
                    }
                    else
                        eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}