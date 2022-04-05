using Leopotam.Ecs;
using Pixelgrid.Components.UI.Turtle;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.UI.Turtle
{
    public class UpdateTurtlePathViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurtlePathViewModel, TurtlePathViewComponent> _filter = null;
        private readonly TurtlePathModel _turtlePathModel = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var viewModel = ref _filter.Get1(index);
                var viewComponent = _filter.Get2(index);
                if (_turtlePathModel.CurrentPath != viewModel.PathIndex)
                {
                    viewModel.PathIndex = _turtlePathModel.CurrentPath;
                    if (viewModel.PathIndex < _turtlePathModel.Path.Count)
                    {
                        viewComponent.View.SetText(new string(_turtlePathModel.Path[viewModel.PathIndex].ToArray()));
                    }
                }
            }
        }
    }
}
