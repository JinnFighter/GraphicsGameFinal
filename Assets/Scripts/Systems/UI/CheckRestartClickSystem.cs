using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid
{
    public class CheckRestartClickSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var data = ref _filter.Get1(index);
                if (data.Button == PointerEventData.InputButton.Left && data.Sender.CompareTag("RestartButton"))
                {
                    var entity = _filter.GetEntity(index);
                    entity.Get<RestartGameEvent>();
                }
            }
        }
    }
}
