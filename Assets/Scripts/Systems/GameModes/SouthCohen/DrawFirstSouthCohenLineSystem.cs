using System.Linq;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class DrawFirstSouthCohenLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<SouthCohenData> _dataFilter = null;
        
        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly LineDataModel _lineDataModel;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _dataFilter)
                {
                    var entity = _dataFilter.GetEntity(index);
                    entity.Get<ClearGridEvent>();
                    ref var drawDataComponent = ref entity.Get<LineDrawData>();
                    var drawingData = 
                        _lineDataModel.LinePoints[0].Select(point => (point, _pixelSpritesContent.FilledSprite)).ToList();

                    drawDataComponent.drawData = drawingData;
                }
            }
        }
    }
}