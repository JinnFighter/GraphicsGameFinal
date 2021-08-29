using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class UpdateDDataSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<Brezenheim_D_Data, TextRef> _filter = null;
        private readonly EcsFilter<UpdateDIndexEvent> _eventFilter = null;

        void IEcsRunSystem.Run() 
        {
            if(!_eventFilter.IsEmpty())
            {
                var indexEvent = _eventFilter.Get1(0);
                foreach(var index in _filter)
                {
                    ref var textRef = ref _filter.Get2(index);
                    textRef.Text.GetComponent<Text>().text = indexEvent.index > 0 ? "-" : "+";
                }
            }
        }
    }
}