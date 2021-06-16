using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GenerateDDataSystem : IEcsRunSystem
    {
        private EcsFilter<GameModeData> _filter;
        private BrezenheimDataContainer _brezenheimDataContainer;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    var entity = _filter.GetEntity(index);
                    ref var dData = ref entity.Get<TextRef>();
                    dData.Text = _brezenheimDataContainer.DText.gameObject;
                }
            }
        }
    }
}