using Leopotam.Ecs;
using System.Collections.Generic;

namespace Pixelgrid
{
    public sealed class UnpauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnpauseEvent> _filter = null;
        private readonly EcsFilter<Paused> _pausedFilter = null;

        private readonly EcsSystems _systems;
        private readonly IEnumerable<string> _pausableSystemsNames;

        public UnpauseSystem(EcsSystems systems, IEnumerable<string> pausableSystemsNames)
        {
            _systems = systems;
            _pausableSystemsNames = pausableSystemsNames;
        }

        void IEcsRunSystem.Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var systemName in _pausableSystemsNames)
                    _systems.SetRunSystemState(_systems.GetNamedRunSystem(systemName), true);

                foreach (var index in _pausedFilter)
                {
                    var entity = _pausedFilter.GetEntity(index);
                    entity.Del<Paused>();
                }
            }
        }
    }
}