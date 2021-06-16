using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateGameFieldPixelsSystem : IEcsRunSystem 
    {
        private EcsFilter<PixelComponent, PixelPosition, PixelRef> _pixelsFilter;
        private EcsFilter<LineDrawData> _drawDataFilter;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _drawDataFilter)
            {
                var drawData = _drawDataFilter.Get1(index);
                var data = drawData.drawData;
                foreach(var pixelIndex in _pixelsFilter)
                {
                    foreach(var item in data)
                    {
                        if(item.Item1 == _pixelsFilter.Get2(pixelIndex).position)
                        {
                            ref var pixelRef = ref _pixelsFilter.Get3(pixelIndex);
                            pixelRef.pixel.GetComponent<GridPixel>().UpdateSprite(item.Item2);
                        }
                    }
                }
            }
        }
    }
}