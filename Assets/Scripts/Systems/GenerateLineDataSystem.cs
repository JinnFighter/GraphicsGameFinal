using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class GenerateLineDataSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;

        private ILinesDataGenerator _lineDataGenerator;

        public GenerateLineDataSystem(ILinesDataGenerator dataGenerator)
        {
            _lineDataGenerator = dataGenerator;
        }

        public void Init() 
        {
            foreach(var index in _gameModeDataFilter)
            {
                var entity = _gameModeDataFilter.GetEntity(index);
                ref var lineData = ref entity.Get<LineData>();
                lineData.LinePoints = _lineDataGenerator.GenerateData(1, 2).ToList();
                lineData.CurrentPoint = 0;
            }
        }
    }
}