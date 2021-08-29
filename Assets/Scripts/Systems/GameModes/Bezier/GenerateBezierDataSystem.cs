using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid
{
    public sealed class GenerateBezierDataSystem : IEcsRunSystem
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;
        private BezierLinesGenerator _lineDataGenerator;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _gameModeDataFilter)
                {
                    var entity = _gameModeDataFilter.GetEntity(index);
                    ref var lineData = ref entity.Get<BezierLineData>();
                    var lines = _lineDataGenerator.GenerateData(3, 5, 3).ToList();

                    lineData.Points = lines;
                    lineData.CurrentPoint = 0;

                    ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                    dataGeneratedEvent.DataCount = lineData.Points.Count;
                }
            }
        }
    }
}