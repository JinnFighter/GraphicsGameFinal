using Leopotam.Ecs;
using System.Linq;

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
                lineData.LinePoints = _lineDataGenerator.GenerateData(2, 5, 5).ToList();
                lineData.CurrentPoint = 0;
            }
        }
    }
}