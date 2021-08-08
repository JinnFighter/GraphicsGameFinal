using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateUiTextSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TextRef, UpdateTextEvent> _filter = null;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _filter)
            {
                var textRef = _filter.Get1(index);
                var textEvent = _filter.Get2(index);

                textRef.Text.text = textEvent.Text;
            }
        }
    }
}