using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Pixelgrid 
{
    public sealed class OnSliderValueChangeSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiSliderChangeEvent> _filter;
        private GameState _gameState;
        private ImageHolderContainer _imageHolderContainer;

        void IEcsRunSystem.Run() 
        {
            if(!_gameState.IsPaused)
            {
                foreach (var index in _filter)
                {
                    ref EcsUiSliderChangeEvent data = ref _filter.Get1(index);
                    var sender = data.Sender;
                    if(sender.CompareTag("ColorSlider"))
                    {
                        var color = _imageHolderContainer.AnswerHolder.color;
                        color.b = data.Value;
                        _imageHolderContainer.AnswerHolder.color = color;
                    }
                }
            }
        }
    }
}