using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class DrawFirstLineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;

        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly LineDataModel _lineDataModel = null;
        private readonly EcsWorld _world;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var entity = _world.NewEntity();
                entity.Get<ClearGridEvent>();
                var linePoints = _lineDataModel.LinePoints;
                ref var drawData = ref entity.Get<LineDrawData>();
                drawData.drawData = new List<(Vector2Int, Sprite)>
                {
                    (linePoints[_lineDataModel.CurrentLine][0], _pixelSpritesContent.LineBeginningSprite),
                    (linePoints[_lineDataModel.CurrentLine][linePoints[_lineDataModel.CurrentLine].Count - 1], _pixelSpritesContent.LineEndSprite)
                };
            }
        }
    }
}