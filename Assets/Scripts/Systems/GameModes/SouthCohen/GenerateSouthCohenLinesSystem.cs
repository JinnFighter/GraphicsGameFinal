using System.Linq;
using Configurations.Script;
using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class GenerateSouthCohenLinesSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<BorderComponent> _borderFilter = null;

        private readonly SouthCohenLinesGenerator _lineDataGenerator = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly LineDataModel _lineDataModel = null;
        private readonly SouthCohenConfigs _configs = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var border = _borderFilter.Get1(0);
                var config = _configs[_difficultyConfiguration.Difficulty];
                var lines = _lineDataGenerator.GenerateData(config.MinLength, config.MaxLength, config.MaxCoordinate, config.LinesCount, border.LeftCorner, border.RightCorner).ToList();
                _lineDataModel.LinePoints.Clear();

                foreach(var line in lines)
                    _lineDataModel.LinePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out _));
                    
                _lineDataModel.CurrentLine = 0;
                _lineDataModel.CurrentPoint = 0;
            }
        }
    }
}