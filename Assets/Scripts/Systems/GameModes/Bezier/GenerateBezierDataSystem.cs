using System.Linq;
using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.Bezier
{
    public sealed class GenerateBezierDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsWorld _world = null;
        private readonly BezierLinesGenerator _lineDataGenerator = null;

        private readonly BezierDataModel _bezierDataModel = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var entity = _world.NewEntity();
                var lines = _lineDataGenerator.GenerateData(3, 5, 3).ToList();

                _bezierDataModel.Points = lines;
                _bezierDataModel.CurrentPoint = 0;

                ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                dataGeneratedEvent.DataCount = lines.Count;
            }
        }
    }
}