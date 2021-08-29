using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateDDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<Brezenheim_D_Data> _filter = null;

        private readonly BrezenheimDataContainer _brezenheimDataContainer = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    var entity = _filter.GetEntity(index);
                    ref var dData = ref entity.Get<TextRef>();
                    dData.Text = _brezenheimDataContainer.DText.gameObject.GetComponent<Text>();
                    ref var updateTextEvent = ref entity.Get<UpdateTextEvent>();
                    updateTextEvent.Text = "?";
                }
            }
        }
    }
}