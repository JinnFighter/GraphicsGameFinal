using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Sprites;

namespace Pixelgrid.Systems.GameField 
{
    public sealed class ClearGridSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<ClearGridEvent> _clearGridEventFilter = null;
        private readonly EcsFilter<PixelComponent, ImageRef> _pixelsFilter = null;
        private readonly PixelSpritesContent _pixelSpritesContent = null;

        void IEcsRunSystem.Run() 
        {
            if(!_clearGridEventFilter.IsEmpty())
            {
                var emptySprite = _pixelSpritesContent.EmptySprite;
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