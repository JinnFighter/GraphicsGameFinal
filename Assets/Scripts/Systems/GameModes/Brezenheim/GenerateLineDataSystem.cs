using System.Collections.Generic;
using System.Linq;
using Configurations.Script;
using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class GenerateLineDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LineData> _gameModeDataFilter = null;
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        
        private readonly LinesGenerator _lineDataGenerator = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly BrezenheimConfigs _brezenheimConfigs = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _gameModeDataFilter)
                {
                    var entity = _gameModeDataFilter.GetEntity(index);
                    ref var lineData = ref entity.Get<LineData>();
                    ref var dData = ref entity.Get<Brezenheim_D_Data>();
                    var config = _brezenheimConfigs.Configs[_difficultyConfiguration.Difficulty];
                    
                    var lines = _lineDataGenerator.GenerateData(config.MinLineLength, config.MaxLineLength, config.LineCount).ToList();
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