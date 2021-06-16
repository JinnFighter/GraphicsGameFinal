using Leopotam.Ecs;
using System;
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
                switch (_difficultyConfiguration.Difficulty)
                {
                    case 1:
                        leftCorner = new Vector2Int(2, 2);
                        rightCorner = new Vector2Int(8, 8);
                        break;
                    case 2:
                        leftCorner = new Vector2Int(2, 2);
                        rightCorner = new Vector2Int(11, 11);
                        break;
                    default:
                        leftCorner = new Vector2Int(3, 3);
                        rightCorner = new Vector2Int(7, 7);
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
                        var pxlPosition = pixelRef.pixel.transform.position;
                        var position = new Vector3(pxlPosition.x * Math.Abs(rightCorner.x - leftCorner.x), pxlPosition.y * Math.Abs(rightCorner.y - leftCorner.y), pxlPosition.z);
                        posOffset = pixelRef.pixel.GetComponent<RectTransform>().rect.width;
                        _border.transform.position = position;
                        _border.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
                    }
                }
            }
        }
    }
}