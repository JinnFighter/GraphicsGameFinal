using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetTurtlePositionSystem : IEcsRunSystem 
    {
        private EcsFilter<TurtleComponent, PixelPosition> _filter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;
        private DifficultyConfiguration _difficultyConfiguration;
        private TurtleConfiguration _turtleConfiguration;
        private TurtleSpritesContainer _turtleSpritesContainer;

        void IEcsRunSystem.Run() 
        {
            var eventReceiver = _eventReceiverFilter.GetEntity(0);
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

                eventReceiver.Get<ClearGridEvent>();
                ref var drawData = ref eventReceiver.Get<LineDrawData>();
                drawData.drawData = new List<(Vector2Int, Sprite)>
                {
                    (position, _turtleSpritesContainer.TurtleRight)
                };
            }
        }
    }
}