using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateLineDataSystem : IEcsRunSystem
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;
        private LinesGenerator _lineDataGenerator;
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private DifficultyConfiguration _difficultyConfiguration;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _gameModeDataFilter)
                {
                    var entity = _gameModeDataFilter.GetEntity(index);
                    ref var lineData = ref entity.Get<LineData>();
                    ref var dData = ref entity.Get<Brezenheim_D_Data>();
                    int minLength;
                    int maxLength;
                    int linesCount;

                    switch(_difficultyConfiguration.Difficulty)
                    {
                        case 1:
                            minLength = 4;
                            maxLength = 8;
                            linesCount = 7;
                            break;
                        case 2:
                            minLength = 5;
                            maxLength = 10;
                            linesCount = 10;
                            break;
                        default:
                            minLength = 2;
                            maxLength = 5;
                            linesCount = 5;
                            break;
                    }

                    var lines = _lineDataGenerator.GenerateData(minLength, maxLength, linesCount).ToList();
                    var lineDatas = new List<List<Vector2Int>>();
                    var dDatas = new List<List<int>>();

                    foreach (var line in lines)
                    {
                        lineDatas.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out var ds));
                        dDatas.Add(ds);
                    }
                    dData.Indexes = dDatas;
                    lineData.LinePoints = lineDatas;
                    lineData.CurrentPoint = 0;
                    lineData.CurrentLine = 0;
                }
            }
        }
    }
}