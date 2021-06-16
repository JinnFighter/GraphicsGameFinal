using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class ResetStatTrackerSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<StatData> _statDataFilter;
        
        void IEcsRunSystem.Run() 
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach(var index in _statDataFilter)
                {
                    ref var statData = ref _statDataFilter.Get1(index);
                    statData.CorrectAnswers = 0;
                    statData.WrongAnswers = 0;
                    statData.TimeSpent = 0f;
                }
            }
        }
    }
}