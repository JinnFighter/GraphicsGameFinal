using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class DrawFirstSouthCohenLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<SouthCohenData, LineData> _dataFilter = null;
        
        private readonly SpritesContainer _spritesContainer = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _dataFilter)
                {
                    var entity = _dataFilter.GetEntity(index);
                    var data = _dataFilter.Get2(index);
                    entity.Get<ClearGridEvent>();
                    ref var drawDataComponent = ref entity.Get<LineDrawData>();
                    var drawingData = 
                        data.LinePoints[0].Select(point => (point, _spritesContainer.FilledSprite)).ToList();

                    drawDataComponent.drawData = drawingData;
                }
            }
        }
    }
}