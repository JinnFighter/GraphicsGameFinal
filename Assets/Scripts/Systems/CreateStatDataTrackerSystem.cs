using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateStatDataTrackerSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<StatData>();
            entity.Get<Stopwatch>();
            entity.Get<StatTrackerStopwatch>();
        }
    }
}