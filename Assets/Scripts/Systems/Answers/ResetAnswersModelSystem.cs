using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.Answers
{
    public class ResetAnswersModelSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameModeDataGeneratedEvent> _filter = null;
        private readonly AnswersModel _answersModel = null;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var dataEvent = _filter.Get1(index);
                _answersModel.MaxAnswerCount = dataEvent.DataCount;
                _answersModel.CurrentAnswerCount = 0;
            }
        }
    }
}
