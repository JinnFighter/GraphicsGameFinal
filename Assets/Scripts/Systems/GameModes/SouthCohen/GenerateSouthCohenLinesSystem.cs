using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateSouthCohenLinesSystem : IEcsRunSystem 
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<BorderComponent> _borderFilter;

        private SouthCohenLinesGenerator _lineDataGenerator;
        private DifficultyConfiguration _difficultyConfiguration;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var border = _borderFilter.Get1(0);
                foreach (var index in _gameModeDataFilter)
                {
                    var entity = _gameModeDataFilter.GetEntity(index);
                    ref var lineData = ref entity.Get<LineData>();
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
                            minLength = 5;
                            b = 9;
                            break;
                    }
                    var lines = _lineDataGenerator.GenerateData(minLength, maxLength, b, linesCount, border.LeftCorner, border.RightCorner).ToList();
                    var linePoints = new List<List<Vector2Int>>();

                    foreach(var line in lines)
                        linePoints.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out _));

                    lineData.LinePoints = linePoints;
                    lineData.CurrentLine = 0;
                    lineData.CurrentPoint = 0;
                }
            }
        }
    }
}