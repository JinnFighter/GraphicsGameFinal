using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid 
{
    sealed class ClearGridSystem : IEcsRunSystem 
    {
        private EcsFilter<ClearGridEvent> _clearGridEventFilter;
        private EcsFilter<PixelComponent, PixelRef> _pixelsFilter;
        private SpritesContainer _spritesContainer;

        void IEcsRunSystem.Run() 
        {
            if(!_clearGridEventFilter.IsEmpty())
            {
                var emptySprite = _spritesContainer.EmptySprite;
                foreach(var index in _pixelsFilter)
                {
                    ref var pixelRef = ref _pixelsFilter.Get2(index);
                    pixelRef.pixel.GetComponent<Image>().sprite = emptySprite;
                }
            }
        }
    }
}