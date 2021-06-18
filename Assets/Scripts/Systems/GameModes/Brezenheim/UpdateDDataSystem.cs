using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class UpdateDDataSystem : IEcsRunSystem 
    {
        private EcsFilter<GameModeData, TextRef> _filter;
        private EcsFilter<GameplayEventReceiver, UpdateDIndexEvent> _eventFilter;

        void IEcsRunSystem.Run() 
        {
            if(!_eventFilter.IsEmpty())
            {
                var indexEvent = _eventFilter.Get2(0);
                foreach(var index in _filter)
                {
                    ref var textRef = ref _filter.Get2(index);
                    textRef.Text.GetComponent<Text>().text = indexEvent.index > 0 ? "-" : "+";
                }
            }
        }
    }
}