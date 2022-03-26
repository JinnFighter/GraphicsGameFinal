using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using Configurations.Script;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateTurtlePathSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TurtlePath> _filter = null;
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly TurtleConfigs _turtleConfigs = null;

        private IDirectionState _direction;

        private readonly List<char> _commands = new List<char>{ 'F', '+', '-' };

        void IEcsRunSystem.Run() 
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach(var index in _filter)
                {
                    ref var turtlePath = ref _filter.Get1(index);
                    var paths = turtlePath.Path;
                    paths.Clear();

                    _direction = new RightDirectionState();

                    for (var i = 0; i < _turtleConfigs.Configs[_difficultyConfiguration.Difficulty].PathCount; i++)
                        paths.Add(GeneratePath());

                    var entity = _filter.GetEntity(index);
                    ref var updateTextEvent = ref entity.Get<UpdateTextEvent>();
                    updateTextEvent.Text = string.Join("", paths[0]);

                    turtlePath.CurrentPath = 0;
                    turtlePath.CurrentSymbol = 0;

                    ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                    dataGeneratedEvent.DataCount = turtlePath.Path.Sum(path => path.Count);
                }
            }
        }

        List<char> GeneratePath()
        {
            var route = new List<char>();
            for (var j = 0; j < _turtleConfigs.Configs[_difficultyConfiguration.Difficulty].PathLength; j++)
            {
                var c = _commands[Random.Range(0, 3)];
                while (j == 0 && c != _commands[0])
                    c = _commands[Random.Range(0, 3)];

                route.Add(c);
            }

            return route;
        }

        bool CanMove(Vector2Int position, List<char> route, out Vector2Int currentPosition, int fieldSize)
        {
            currentPosition = new Vector2Int(position.x,  position.y);
            foreach (var symbol in route)
            {
                switch(symbol)
                {
                    case 'F':
                        var tempPosition = _direction.Move(currentPosition);
                        if (tempPosition.x < 0 || tempPosition.x >= fieldSize ||
                            tempPosition.y < 0 || tempPosition.y >= fieldSize)
                        {
                            return false;
                        }
                        currentPosition = tempPosition;
                        break;
                    case '+':
                        _direction = _direction.RotateLeft(out _);
                        break;
                    case '-':
                        _direction = _direction.RotateRight(out _);
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }
    }
}