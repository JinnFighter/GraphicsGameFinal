using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CheckBezierAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter = null;
        private readonly EcsFilter<BezierLineData> _gameModeDataFilter = null;

        void IEcsRunSystem.Run()
        {
            ref var lineDataComponent = ref _gameModeDataFilter.Get1(0);
            var lineDatas = lineDataComponent.Points;
            var eventReceiver = _gameModeDataFilter.GetEntity(0);
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