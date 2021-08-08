using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class CreateTurtlePathSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<GameModeData> _filter = null;

        private readonly Text _pathText = null;

        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var turtlePathComponent = ref entity.Get<TurtlePath>();
                turtlePathComponent.Path = new List<List<char>>();
                ref var textRef = ref entity.Get<TextRef>();
                textRef.Text = _pathText;
            }
        }
    }
}