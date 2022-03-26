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
        private readonly BrezenheimDataModel _brezenheimDataModel = null;

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
                _brezenheimDataModel.LinePoints.Clear();

                foreach(var line in lines)
                    _brezenheimDataModel.LinePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out _));
                    
                _brezenheimDataModel.CurrentLine = 0;
                _brezenheimDataModel.CurrentPoint = 0;
            }
        }
    }
}