using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems
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
