using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateImageSpritesSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ImageRef, UpdateSpriteImageEvent> _filter = null;
        
        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                var imageRef = _filter.Get1(index);
                var spriteEvent = _filter.Get2(index);
                imageRef.Image.sprite = spriteEvent.Sprite;
            }
        }
    }
}