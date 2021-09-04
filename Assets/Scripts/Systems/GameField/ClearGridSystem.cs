using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class ClearGridSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<ClearGridEvent> _clearGridEventFilter = null;
        private readonly EcsFilter<PixelComponent, ImageRef> _pixelsFilter = null;
        private readonly SpritesContainer _spritesContainer = null;

        void IEcsRunSystem.Run() 
        {
            if(!_clearGridEventFilter.IsEmpty())
            {
                var emptySprite = _spritesContainer.EmptySprite;
                foreach(var index in _pixelsFilter)
                {
                    var entity = _pixelsFilter.GetEntity(index);
                    ref var updateSpriteEvent = ref entity.Get<UpdateSpriteImageEvent>();
                    updateSpriteEvent.Sprite = emptySprite;
                }
            }
        }
    }
}