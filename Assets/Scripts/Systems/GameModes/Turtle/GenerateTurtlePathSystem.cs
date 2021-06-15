using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateTurtlePathSystem : IEcsRunSystem 
    {
        private EcsFilter<TurtlePath> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<TurtleComponent, PixelPosition> _turtleFilter;
        private TurtleConfiguration _turtleConfiguration;
        private GameFieldConfiguration _gameFieldConfiguration;
        private Text _pathText;
        private IDirectionState _direction;

        private List<char> _commands = new List<char>{ 'F', '+', '-' };

        void IEcsRunSystem.Run() 
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach(var index in _filter)
                {
                    ref var turtlePath = ref _filter.Get1(index);
                    var paths = turtlePath.Path;
                    paths.Clear();

                    var originalPositionComponent = _turtleFilter.Get2(0);
                    var originalPosition = originalPositionComponent.position;
                    var currentPosition = new Vector2Int(originalPosition.x, originalPosition.y);

                    _direction = new RightDirectionState();
                    for (var i = 0; i < _turtleConfiguration.PathsCount; i++)
                    {
                        List<char> route;
                        do
                        {
                            route = GeneratePath();
                        }
                        while (!CanMove(currentPosition, route, out currentPosition));
                        paths.Add(route);
                    }
                    _pathText.text = string.Join("", paths[0]);

                    turtlePath.CurrentPath = 0;
                    turtlePath.CurrentSymbol = 0;
                }
            }
        }

        List<char> GeneratePath()
        {
            var route = new List<char>();
            for (var j = 0; j < _turtleConfiguration.PathLength; j++)
            {
                var c = _commands[Random.Range(0, 3)];
                while (j == 0 && c != _commands[0])
                    c = _commands[Random.Range(0, 3)];

                route.Add(c);
            }

            return route;
        }

        bool CanMove(Vector2Int position, List<char> route, out Vector2Int currentPosition)
        {
            currentPosition = new Vector2Int(position.x,  position.y);
            foreach (var symbol in route)
            {
                switch(symbol)
                {
                    case 'F':
                        var tempPosition = _direction.Move(currentPosition);
                        if (tempPosition.x < 0 || tempPosition.x >= _gameFieldConfiguration.fieldSize ||
                            tempPosition.y < 0 || tempPosition.y >= _gameFieldConfiguration.fieldSize)
                        {
                            return false;
                        }
                        currentPosition = tempPosition;
                        break;
                    case '+':
                        _direction = _direction.RotateLeft();
                        break;
                    case '-':
                        _direction = _direction.RotateRight();
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }
    }
}