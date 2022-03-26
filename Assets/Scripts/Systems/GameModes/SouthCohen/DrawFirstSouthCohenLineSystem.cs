using System.Linq;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class DrawFirstSouthCohenLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsWorld _world = null;

        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly LineDataModel _lineDataModel = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var entity = _world.NewEntity();
                entity.Get<ClearGridEvent>();
                ref var drawDataComponent = ref entity.Get<LineDrawData>();
                var drawingData = 
                    _lineDataModel.LinePoints[0].Select(point => (point, _pixelSpritesContent.FilledSprite)).ToList();

                drawDataComponent.drawData = drawingData;
            }
        }
    }
}