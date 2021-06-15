using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateTurtleSpritesSystem : IEcsRunSystem 
    {
        private EcsFilter<GameplayEventReceiver, TurtleCommand> _filter;
        private EcsFilter<TurtleComponent, PixelPosition> _turtleFilter;


        private TurtleSpritesContainer _turtleSpritesContainer;
        private SpritesContainer _spritesContainer;

        void IEcsRunSystem.Run()
        {
            foreach(var index in _filter)
            {
                var eventReceiver = _filter.GetEntity(index);
                if(eventReceiver.Has<CorrectAnswerEvent>())
                {
                    ref var turtle = ref _turtleFilter.Get1(0);
                    ref var turtlePosition = ref _turtleFilter.Get2(0);
                    var command = eventReceiver.Get<TurtleCommand>();
                    LookDirection direction;
                    var drawData = new List<(Vector2Int, Sprite)>();

                    switch (command.CommandSymbol)
                    {
                        case 'F':
                            var nextPosition = turtle.DirectionState.Move(turtlePosition.position);
                            drawData.Add((turtlePosition.position, _spritesContainer.EmptySprite));
                            drawData.Add((nextPosition, turtle.CurrentSprite));
                            turtlePosition.position = nextPosition;
                            break;
                        case '+':
                            turtle.DirectionState = turtle.DirectionState.RotateLeft(out direction);
                            turtle.CurrentSprite = _turtleSpritesContainer.GetSprite(direction);
                            drawData.Add((turtlePosition.position, turtle.CurrentSprite));
                            break;
                        case '-':
                            turtle.DirectionState = turtle.DirectionState.RotateRight(out direction);
                            turtle.CurrentSprite = _turtleSpritesContainer.GetSprite(direction);
                            drawData.Add((turtlePosition.position, turtle.CurrentSprite));
                            break;
                        default:
                            break;
                    }

                    if(drawData.Any())
                    {
                        ref var drawingData = ref eventReceiver.Get<LineDrawData>();
                        drawingData.drawData = drawData;
                    }
                }
            }
        }
    }
}