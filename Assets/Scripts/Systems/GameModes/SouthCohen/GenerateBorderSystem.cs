using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateBorderSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _filter;
        private EcsFilter<PixelComponent, PixelPosition, PixelRef> _pixelFilter;

        private DifficultyConfiguration _difficultyConfiguration;
        private GameObject _border;

        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var border = ref entity.Get<BorderComponent>();
                Vector2Int leftCorner;
                Vector2Int rightCorner;
                float posOffset;
                float scaleMultiplier;
                switch (_difficultyConfiguration.Difficulty)
                {
                    case 1:
                        leftCorner = new Vector2Int(2, 2);
                        rightCorner = new Vector2Int(8, 8);
                        posOffset = 9.5f;
                        scaleMultiplier = 7.5f;
                        break;
                    case 2:
                        leftCorner = new Vector2Int(2, 2);
                        rightCorner = new Vector2Int(11, 11);
                        posOffset = 14.5f;
                        scaleMultiplier = 10;
                        break;
                    default:
                        leftCorner = new Vector2Int(3, 3);
                        rightCorner = new Vector2Int(7, 7);
                        posOffset = 12.5f;
                        scaleMultiplier = 10;
                        break;
                }

                border.LeftCorner = leftCorner;
                border.RightCorner = rightCorner;

                foreach(var pixelIndex in _pixelFilter)
                {
                    var pixelPosition = _pixelFilter.Get2(pixelIndex);
                    if(pixelPosition.position.Equals(leftCorner))
                    {
                        var pixelRef = _pixelFilter.Get3(pixelIndex);
                        var position = pixelRef.pixel.transform.position;
                        position.x += posOffset;
                        position.y -= posOffset;
                        _border.transform.position = position;
                        var scale = _border.transform.localScale;
                        scale.x *= scaleMultiplier;
                        scale.y *= scaleMultiplier;
                        _border.transform.localScale = scale;
                    }
                }
            }
        }
    }
}