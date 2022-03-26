using System;
using Configurations.Script;
using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class GenerateBorderSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<PixelComponent, PixelPosition, PixelRef> _pixelFilter = null;

        private readonly SouthCohenConfigs _southCohenConfigs = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly GameObject _border = null;

        public void Init()
        {
            float posOffset;

            var leftCorner = _southCohenConfigs[_difficultyConfiguration.Difficulty].BorderLeftCorner;
            var rightCorner = _southCohenConfigs[_difficultyConfiguration.Difficulty].BorderRightCorner;

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