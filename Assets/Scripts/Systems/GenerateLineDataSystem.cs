using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class GenerateLineDataSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;
        private LinesGenerator _lineDataGenerator;

        public void Init() 
        {
            foreach(var index in _gameModeDataFilter)
            {
                var entity = _gameModeDataFilter.GetEntity(index);
                ref var lineData = ref entity.Get<LineData>();
                ref var dData = ref entity.Get<Brezenheim_D_Data>();
                var lines = _lineDataGenerator.GenerateData(2, 5, 5).ToList();
                var lineDatas = new List<List<Vector2Int>>();
                var dDatas = new List<List<int>>();

                foreach(var line in lines)
                {
                    lineDatas.Add(Algorithms.GetBrezenheimLineData(line.Item1, line.Item2, out var ds));
                    dDatas.Add(ds);
                }
                lineData.LinePoints = lineDatas;
                lineData.CurrentPoint = 0;
                lineData.CurrentLine = 0;
            }
        }
    }
}