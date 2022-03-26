using System.Linq;
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

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var border = _borderFilter.Get1(0);
                int b;
                int minLength;
                int maxLength;
                int linesCount;
                switch (_difficultyConfiguration.Difficulty)
                {
                    case 1:
                        linesCount = 7;
                        maxLength = 10;
                        minLength = 8;
                        b = 14;
                        break;
                    case 2:
                        linesCount = 10;
                        maxLength = 11;
                        minLength = 10;
                        b = 19;
                        break;
                    default:
                        linesCount = 5;
                        maxLength = 8;
                        minLength = 6;
                        b = 9;
                        break;
                }
                var lines = _lineDataGenerator.GenerateData(minLength, maxLength, b, linesCount, border.LeftCorner, border.RightCorner).ToList();
                _lineDataModel.LinePoints.Clear();

                foreach(var line in lines)
                    _lineDataModel.LinePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out _));
                    
                _lineDataModel.CurrentLine = 0;
                _lineDataModel.CurrentPoint = 0;
            }
        }
    }
}