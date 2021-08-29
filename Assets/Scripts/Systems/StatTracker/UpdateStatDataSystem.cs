using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class UpdateStatDataSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<StatData, Stopwatch, StatTrackerStopwatch> _statDataFilter = null;
        private readonly EcsFilter<CorrectAnswerEvent> _correctAnswerFilter = null;
        private readonly EcsFilter<WrongAnswerEvent> _wrongAnswerFilter = null;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _statDataFilter)
            {
                ref var statData = ref _statDataFilter.Get1(index);
                var stopwatch = _statDataFilter.Get2(index);
                statData.TimeSpent = stopwatch.currentTime;
                if (!_correctAnswerFilter.IsEmpty())
                    statData.CorrectAnswers += _correctAnswerFilter.GetEntitiesCount();
                if (!_wrongAnswerFilter.IsEmpty())
                    statData.WrongAnswers += _wrongAnswerFilter.GetEntitiesCount();
            }
        }
    }
}