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
        private readonly BrezenheimDataModel _brezenheimDataModel = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var config = _brezenheimConfigs.Configs[_difficultyConfiguration.Difficulty];
                    
                var lines = _lineDataGenerator.GenerateData(config.MinLineLength, config.MaxLineLength, config.LineCount).ToList();
                _brezenheimDataModel.Indexes.Clear();
                _brezenheimDataModel.LinePoints.Clear();

                foreach (var line in lines)
                {
                    _brezenheimDataModel.LinePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out var ds));
                    _brezenheimDataModel.Indexes.Add(ds);
                }
                _brezenheimDataModel.CurrentPoint = 0;
                _brezenheimDataModel.CurrentLine = 0;

                var entity = _world.NewEntity();
                ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                dataGeneratedEvent.DataCount = _brezenheimDataModel.LinePoints.Sum(linePoint => linePoint.Count);
            }
        }
    }
}