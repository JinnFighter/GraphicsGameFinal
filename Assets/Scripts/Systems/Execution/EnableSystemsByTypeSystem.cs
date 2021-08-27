using Leopotam.Ecs;

namespace Pixelgrid.Systems.Execution
{
    public class EnableSystemsByTypeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnableSystemTypeEvent> _filter = null;

        private readonly EcsSystems _systems;
        private readonly SystemNamesContainer _systemNamesContainer;

        public EnableSystemsByTypeSystem(EcsSystems systems, SystemNamesContainer container)
        {
            _systems = systems;
            _systemNamesContainer = container;
        }
        
        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                var enableEvent = _filter.Get1(index);
                var systems = _systemNamesContainer.Systems;
                
                if (systems.TryGetValue(enableEvent.SystemType, out var systemNames))
                {
                    foreach (var systemName in systemNames)
                        _systems.SetRunSystemState(_systems.GetNamedRunSystem(systemName), true);
                }
            }
        }
    }
}