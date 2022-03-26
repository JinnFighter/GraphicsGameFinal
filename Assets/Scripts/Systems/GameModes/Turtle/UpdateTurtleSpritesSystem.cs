using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Pixelgrid.Configurations.Script;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Turtle 
{
    public sealed class UpdateTurtleSpritesSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TurtleComponent, TurtlePath, PixelPosition, TurtleCommand, CorrectAnswerEvent> _filter = null;
        
        private readonly TurtleSpritesContent _turtleSpritesContent = null;
        private readonly GameFieldConfigs _gameFieldConfigs = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly SpritesContainer _spritesContainer = null;

        void IEcsRunSystem.Run()
        {
            var fieldSize = _gameFieldConfigs.Configs[_difficultyConfiguration.Difficulty].FieldSize;
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
                        if(nextPosition.x >= 0 && nextPosition.x < fieldSize && nextPosition.y >= 0 && nextPosition.y < fieldSize)
                        {
                            drawData.Add((turtlePosition.position, _spritesContainer.EmptySprite));
                            drawData.Add((nextPosition, turtle.CurrentSprite));
                            turtlePosition.position = nextPosition;
                        }
                        break;
                    case '+':
                        turtle.DirectionState = turtle.DirectionState.RotateLeft(out direction);
                        turtle.CurrentSprite = _turtleSpritesContent[direction];
                        drawData.Add((turtlePosition.position, turtle.CurrentSprite));
                        break;
                    case '-':
                        turtle.DirectionState = turtle.DirectionState.RotateRight(out direction);
                        turtle.CurrentSprite = _turtleSpritesContent[direction];
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