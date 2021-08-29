using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<GameModeDataGeneratedEvent> _dataGeneratedEventFilter = null;

        private readonly ProgressBar _progressBar = null;

        void IEcsRunSystem.Run()
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _dataGeneratedEventFilter)
                {
                    var dataGeneratedEvent = _dataGeneratedEventFilter.Get1(index);
                    _progressBar.MaxValue = dataGeneratedEvent.DataCount;
                    _progressBar.CurrentValue = 0;
                    _progressBar.Color = new Color32(220, 221, 225, 255);
                }
            }
        }
    }
}