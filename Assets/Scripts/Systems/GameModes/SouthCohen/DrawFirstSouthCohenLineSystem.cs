using System.Linq;
using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Sprites;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class DrawFirstSouthCohenLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<LineData, SouthCohenData> _dataFilter = null;
        
        private readonly PixelSpritesContent _pixelSpritesContent = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _dataFilter)
                {
                    var entity = _dataFilter.GetEntity(index);
                    var data = _dataFilter.Get1(index);
                    entity.Get<ClearGridEvent>();
                    ref var drawDataComponent = ref entity.Get<LineDrawData>();
                    var drawingData = 
                        data.LinePoints[0].Select(point => (point, _pixelSpritesContent.FilledSprite)).ToList();

                    drawDataComponent.drawData = drawingData;
                }
            }
        }
    }
}