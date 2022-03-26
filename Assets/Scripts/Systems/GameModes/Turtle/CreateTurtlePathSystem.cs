using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using UnityEngine.UI;

namespace Pixelgrid.Systems.GameModes.Turtle 
{
    public sealed class CreateTurtlePathSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<TurtleComponent> _filter = null;

        private readonly Text _pathText = null;
        private readonly TurtlePathModel _turtlePathModel = null;

        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                _turtlePathModel.Path = new List<List<char>>();
                ref var textRef = ref entity.Get<TextRef>();
                textRef.Text = _pathText;
            }
        }
    }
}