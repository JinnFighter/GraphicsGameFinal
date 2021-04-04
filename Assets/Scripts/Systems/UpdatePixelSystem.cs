using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdatePixelSystem : IEcsRunSystem 
    {
        private SpritesContainer _spritesContainer;
        private EcsFilter<PixelComponent, PixelRef, PixelClickedEvent> _filter;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _filter)
            {
                ref var pixelRef = ref _filter.Get2(index);
                var pixel = pixelRef.pixel.GetComponent<GridPixel>();
                pixel.UpdateSprite(_spritesContainer.FilledSprite);
            }
        }
    }
}