using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Bezier 
{
    public sealed class DrawFirstBezierLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsWorld _world = null;

        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly BezierDataModel _bezierDataModel = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var points = _bezierDataModel.Points;
                var entity = _world.NewEntity();
                entity.Get<ClearGridEvent>();
                ref var drawData = ref entity.Get<LineDrawData>();
                var drawList = new List<(Vector2Int, Sprite)>();
                for(var i = 0; i < points.Count; i++)
                {
                    Sprite sprite;
                        
                    if(i == 0)
                        sprite = _pixelSpritesContent.LineBeginningSprite;
                    else if(i == points.Count - 1)
                        sprite = _pixelSpritesContent.LineEndSprite;
                    else
                        sprite =  _pixelSpritesContent.FilledSprite;
                        
                    drawList.Add((points[i], sprite));
                }
                drawData.drawData = drawList;
            }
        }
    }
}