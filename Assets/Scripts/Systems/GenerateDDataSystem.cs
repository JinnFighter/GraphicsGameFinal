using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class GenerateDDataSystem : IEcsInitSystem, IEcsRunSystem, ICommand 
    {
        private EcsFilter<GameModeData> _filter;
        private BrezenheimDataContainer _brezenheimDataContainer;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Execute()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var dData = ref entity.Get<TextRef>();
                dData.Text = _brezenheimDataContainer.DText.gameObject;
            }
        }

        public void Init() => Execute(); 

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
                Execute();
        }
    }
}