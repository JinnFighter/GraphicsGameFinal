using Leopotam.Ecs;
using Pixelgrid.Components.UI.Turtle;
using Pixelgrid.UI.Views;

namespace Pixelgrid.Systems.UI.Turtle
{
    public class InitTurtlePathViewSystem : IEcsInitSystem
    {
        private readonly EcsWorld _ecsWorld = null;
        private readonly TurtlePathView _turtlePathView = null;
        
        public void Init()
        {
            var entity = _ecsWorld.NewEntity();
            entity.Get<TurtlePathViewModel>();
            ref var viewComponent = ref entity.Get<TurtlePathViewComponent>();
            viewComponent.View = _turtlePathView;
        }
    }
}
