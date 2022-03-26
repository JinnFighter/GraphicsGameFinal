using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.Bezier 
{
    public sealed class CheckBezierAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter = null;
        private readonly EcsWorld _world = null;
        private readonly BezierDataModel _bezierDataModel = null;

        void IEcsRunSystem.Run()
        {
            var lineDatas = _bezierDataModel.Points;
            var eventReceiver = _world.NewEntity();
            foreach (var pixelIndex in _pixelsClickedFilter)
            {
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if (_bezierDataModel.CurrentPoint >= lineDatas.Count)
                    eventReceiver.Get<GameOverEvent>();
                else
                {
                    if(position.Equals(lineDatas[_bezierDataModel.CurrentPoint]))
                    {
                        eventReceiver.Get<CorrectAnswerEvent>();
                        _bezierDataModel.CurrentPoint++;
                        if (_bezierDataModel.CurrentPoint >= lineDatas.Count)
                            eventReceiver.Get<GameOverEvent>();
                    }
                    else
                        eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}