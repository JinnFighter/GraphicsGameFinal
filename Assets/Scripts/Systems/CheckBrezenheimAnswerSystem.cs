using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CheckBrezenheimAnswerSystem : IEcsRunSystem 
    {
        private EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter;
        private EcsFilter<GameModeData, LineData> _gameModeDataFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        void IEcsRunSystem.Run() 
        {
            ref var lineDataComponent = ref _gameModeDataFilter.Get2(0);
            var lineData = lineDataComponent.LinePoints;
            var currentLine = lineDataComponent.CurrentPoint;
            var eventReceiver = _eventReceiverFilter.GetEntity(0);

            foreach(var pixelIndex in _pixelsClickedFilter)
            {
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if(currentLine >= lineData.Count)
                {
                    eventReceiver.Get<GameOverEvent>();
                    return;
                }
            }
        }
    }
}