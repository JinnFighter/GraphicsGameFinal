using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.Answers
{
    public class UpdateAnswersModelSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CorrectAnswerEvent> _filter = null;
        private readonly AnswersModel _answersModel = null;
        
        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                _answersModel.CurrentAnswerCount++;
            }
        }
    }
}
