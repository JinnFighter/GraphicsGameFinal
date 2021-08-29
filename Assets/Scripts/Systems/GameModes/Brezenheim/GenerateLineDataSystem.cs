using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateLineDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GameModeData> _gameModeDataFilter = null;
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        
        private readonly LinesGenerator _lineDataGenerator = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;

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
                            minLength = 3;
                            maxLength = 6;
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

                    ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                    dataGeneratedEvent.DataCount = lineData.LinePoints.Sum(linePoint => linePoint.Count);
                }
            }
        }
    }
}