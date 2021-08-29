using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetTurtlePositionSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<TurtleComponent, PixelPosition> _filter = null;

        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly TurtleConfiguration _turtleConfiguration = null;
        private readonly TurtleSpritesContainer _turtleSpritesContainer = null;

        void IEcsRunSystem.Run() 
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    ref var positionComponent = ref _filter.Get2(index);
                    var position = new Vector2Int(0, 0);
                    int pathLength;
                    int pathsCount;
                    switch (_difficultyConfiguration.Difficulty)
                    {
                        case 1:
                            position.x = 6;
                            position.y = 6;
                            pathLength = 7;
                            pathsCount = 7;
                            break;
                        case 2:
                            position.x = 7;
                            position.y = 7;
                            pathLength = 10;
                            pathsCount = 10;
                            break;
                        default:
                            position.x = 4;
                            position.y = 4;
                            pathLength = 5;
                            pathsCount = 5;
                            break;
                    }
                    positionComponent.position = position;

                    _turtleConfiguration.PathLength = pathLength;
                    _turtleConfiguration.PathsCount = pathsCount;
                    
                    var entity = _filter.GetEntity(index);
                    entity.Get<ClearGridEvent>();
                    ref var drawData = ref entity.Get<LineDrawData>();
                    drawData.drawData = new List<(Vector2Int, Sprite)>
                {
                    (position, _turtleSpritesContainer.TurtleRight)
                };

                    ref var turtle = ref _filter.Get1(index);
                    turtle.CurrentSprite = _turtleSpritesContainer.TurtleRight;
                    turtle.DirectionState = new RightDirectionState();
                }
            }
        }
    }
}