using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class ShowEndgameScreenSystem : IEcsRunSystem 
    {
        private EcsFilter<GameOverEvent> _eventFilter;
        private EcsFilter<StatData> _statDataFilter;
        private EndgameScreenPresenter _endgamePresenter;
        void IEcsRunSystem.Run () 
        {
            if(!_eventFilter.IsEmpty())
            {
                _endgamePresenter.gameObject.SetActive(true);
                foreach(var index in _statDataFilter)
                {
                    var data = _statDataFilter.Get1(index);
                    var statsDataModel = new StatsData
                    {
                        CorrectAnswers = data.CorrectAnswers,
                        WrongAnswers = data.WrongAnswers,
                        TimeSpent = data.TimeSpent
                    };

                    _endgamePresenter.UpdateData(statsDataModel);
                }
            }
        }
    }
}