using Leopotam.Ecs;

namespace Pixelgrid
{
    public class CreateBezierDataSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<BezierLineData>();
        }
    }
}