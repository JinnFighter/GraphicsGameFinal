using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateTurtlePathSystem : IEcsRunSystem 
    {
        private EcsFilter<TurtlePath> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private Text _pathText;

        private List<char> _commands = new List<char>{ 'F', '+', '-' };

        void IEcsRunSystem.Run() 
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach(var index in _filter)
                {
                    ref var turtlePath = ref _filter.Get1(index);
                    var paths = turtlePath.Path;
                    for (var i = 0; i < paths.Count; i++)
                    {
                        var route = new List<char>();
                        for (var j = 0; j < paths[i].Count; j++)
                        {
                            var c = _commands[UnityEngine.Random.Range(0, 3)];
                            while (j == 0 && c != _commands[0])
                                c = _commands[UnityEngine.Random.Range(0, 3)];

                            route.Add(c);
                        }

                        paths.Add(route);
                    }
                    _pathText.text = string.Join("", paths[0]);
                }
            }
        }
    }
}