using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateGameFieldPixelsSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelComponent, PixelPosition, PixelRef, ImageRef> _pixelsFilter = null;
        private readonly EcsFilter<LineDrawData> _drawDataFilter = null;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _drawDataFilter)
            {
                var drawData = _drawDataFilter.Get1(index);
                var data = drawData.drawData;
                foreach(var pixelIndex in _pixelsFilter)
                {
                    foreach(var (vector2Int, sprite) in data)
                    {
                        if(vector2Int == _pixelsFilter.Get2(pixelIndex).position)
                        {
                            var entity = _pixelsFilter.GetEntity(pixelIndex);
                            ref var updateSpriteEvent = ref entity.Get<UpdateSpriteImageEvent>();
                            updateSpriteEvent.Sprite = sprite;
                        }
                    }
                }
            }
        }
    }
}