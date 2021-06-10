using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Pixelgrid 
{
    public sealed class CheckPauseClickSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiClickEvent> _filter;
        private GameState _gameState;
        private UiScreenContainer _screenContainer;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref EcsUiClickEvent data = ref _filter.Get1(index);
                if (data.Sender.CompareTag("PauseButton"))
                    _gameState.IsPaused = _screenContainer.GetCount() > 0 ? true : false;   
            }
        }
    }
}