using Leopotam.Ecs;

namespace Pixelgrid.Systems.Execution
{
    public class DisableSystemsByTypeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DisableSystemTypeEvent> _filter = null;

        private readonly EcsSystems _systems;
        private readonly SystemNamesContainer _systemNamesContainer;

        public DisableSystemsByTypeSystem(EcsSystems systems, SystemNamesContainer container)
        {
            _systems = systems;
            _systemNamesContainer = container;
        }
        
        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                var disableEvent = _filter.Get1(index);
                var systems = _systemNamesContainer.Systems;
                if (systems.TryGetValue(disableEvent.SystemsType, out var systemNames))
                {
                    foreach (var systemName in systemNames)
                        _systems.SetRunSystemState(_systems.GetNamedRunSystem(systemName), false);
                }
            }
        }
    }
}