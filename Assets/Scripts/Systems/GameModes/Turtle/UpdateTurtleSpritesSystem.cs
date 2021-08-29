using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateTurtleSpritesSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TurtleComponent, TurtlePath, PixelPosition, TurtleCommand, CorrectAnswerEvent> _filter = null;

        private readonly TurtleSpritesContainer _turtleSpritesContainer = null;
        private readonly GameFieldConfiguration _gameFieldConfiguration = null;
        private readonly SpritesContainer _spritesContainer = null;

        void IEcsRunSystem.Run()
        {
            foreach(var index in _filter)
            {
                ref var turtle = ref _filter.Get1(index);
                ref var turtlePosition = ref _filter.Get3(index);
                var command = _filter.Get4(index);
                LookDirection direction;
                var drawData = new List<(Vector2Int, Sprite)>();

                switch (command.CommandSymbol)
                {
                    case 'F':
                        var nextPosition = turtle.DirectionState.Move(turtlePosition.position);
                        if(_gameFieldConfiguration.IsWithinField(nextPosition))
                        {
                            drawData.Add((turtlePosition.position, _spritesContainer.EmptySprite));
                            drawData.Add((nextPosition, turtle.CurrentSprite));
                            turtlePosition.position = nextPosition;
                        }
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
                }

                if(drawData.Any())
                {
                    var entity = _filter.GetEntity(index);
                    ref var drawingData = ref entity.Get<LineDrawData>();
                    drawingData.drawData = drawData;
                }
            }
        }
    }
}