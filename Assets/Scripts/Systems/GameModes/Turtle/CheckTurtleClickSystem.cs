using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid 
{
    public sealed class CheckTurtleClickSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;
        private readonly EcsFilter<TurtleComponent, TurtlePath, PixelPosition> _turtleFilter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var index in _filter)
            {
                ref var data = ref _filter.Get1(index);
                var sender = data.Sender;
                
                if (data.Button == PointerEventData.InputButton.Left &&
                    sender.TryGetComponent<TurtleControl>(out var turtleControl))
                {
                    foreach (var turtleIndex in _turtleFilter)
                    {
                        var entity = _turtleFilter.GetEntity(turtleIndex);
                        ref var turtleCommand = ref entity.Get<TurtleCommand>();
                        turtleCommand.CommandSymbol = turtleControl.CommandSymbol;
                    }
                }
            }
        }
    }
}