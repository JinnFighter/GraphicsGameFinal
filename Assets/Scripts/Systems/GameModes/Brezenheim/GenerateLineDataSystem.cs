using System.Linq;
using Configurations.Script;
using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class GenerateLineDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsWorld _world = null;
        
        private readonly LinesGenerator _lineDataGenerator = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly BrezenheimConfigs _brezenheimConfigs = null;
        private readonly LineDataModel _lineDataModel = null;
        private readonly AnswersModel _answersModel = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var config = _brezenheimConfigs[_difficultyConfiguration.Difficulty];
                    
                var lines = _lineDataGenerator.GenerateData(config.MinLineLength, config.MaxLineLength, config.LineCount).ToList();
                _lineDataModel.Indexes.Clear();
                _lineDataModel.LinePoints.Clear();

                foreach (var line in lines)
                {
                    _lineDataModel.LinePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out var ds));
                    _lineDataModel.Indexes.Add(ds);
                }
                _lineDataModel.CurrentPoint = 0;
                _lineDataModel.CurrentLine = 0;

                var entity = _world.NewEntity();
                ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                dataGeneratedEvent.DataCount = _lineDataModel.LinePoints.Sum(linePoint => linePoint.Count);
                _answersModel.MaxAnswerCount = dataGeneratedEvent.DataCount;
                _answersModel.CurrentAnswerCount = 0;
            }
        }
    }
}