using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class GenerateDDataSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _filter;
        private BrezenheimDataContainer _brezenheimDataContainer;
        
        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var dData = ref entity.Get<TextRef>();
                dData.Text = _brezenheimDataContainer.DText.gameObject;
            }
        }
    }
}