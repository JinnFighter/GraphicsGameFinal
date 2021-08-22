using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid 
{
    public sealed class CheckPauseClickSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;
        private readonly EcsFilter<Paused> _pausedFilter = null;

        private readonly UiScreenContainer _screenContainer;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var data = _filter.Get1(index);
                if (data.Button == PointerEventData.InputButton.Left && data.Sender.CompareTag("PauseButton"))
                {
                    var entity = _filter.GetEntity(index);

                    var hasActiveScreens = _screenContainer.GetCount() > 0;
                    var hasPauseComponent = _pausedFilter.IsEmpty();

                    var canCreateEvent = hasActiveScreens == hasPauseComponent;  //Pause if has screens and doesn't have Paused component, Unpause if has no screens and has Paused component

                    if (canCreateEvent)
                    {
                        if (hasActiveScreens)
                            entity.Get<PauseEvent>();
                        else
                            entity.Get<UnpauseEvent>();
                    }
                }
            }
        }
    }
}