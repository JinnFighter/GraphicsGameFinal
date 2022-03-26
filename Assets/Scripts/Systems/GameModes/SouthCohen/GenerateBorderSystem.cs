using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class GenerateBorderSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<PixelComponent, PixelPosition, PixelRef> _pixelFilter = null;
        private readonly EcsWorld _world = null;

        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly GameObject _border = null;

        public void Init()
        {
            var entity = _world.NewEntity();
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