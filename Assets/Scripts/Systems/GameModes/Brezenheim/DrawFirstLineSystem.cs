using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class DrawFirstLineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<LineData> _dataFilter = null;
        
        private readonly PixelSpritesContent _pixelSpritesContent = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _dataFilter)
                {
                    var entity = _dataFilter.GetEntity(index);
                    var data = _dataFilter.Get1(index);
                    entity.Get<ClearGridEvent>();
                    ref var drawData = ref entity.Get<LineDrawData>();
                    drawData.drawData = new List<(Vector2Int, Sprite)>
                    {
                        (data.LinePoints[data.CurrentLine][0], _pixelSpritesContent.LineBeginningSprite),
                        (data.LinePoints[data.CurrentLine][data.LinePoints[data.CurrentLine].Count - 1], _pixelSpritesContent.LineEndSprite)
                    };
                }
            }
        }
    }
}