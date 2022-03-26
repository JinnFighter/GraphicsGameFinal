using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.Turtle 
{
    public sealed class CheckTurtleAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TurtleComponent, PixelPosition, TurtleCommand> _filter = null;
        private readonly TurtlePathModel _turtlePathModel = null;

        void IEcsRunSystem.Run()
        { 
            foreach(var index in _filter)
            {
                var turtlePathEntity = _filter.GetEntity(index);
                
                var gameplayEventReceiver = _filter.GetEntity(index);

                var turtleCommand = _filter.Get3(index);
                var symbol = turtleCommand.CommandSymbol;
                if(symbol == _turtlePathModel.Path[_turtlePathModel.CurrentPath][_turtlePathModel.CurrentSymbol])
                {
                    gameplayEventReceiver.Get<CorrectAnswerEvent>();
                    _turtlePathModel.CurrentSymbol++;
                    if(_turtlePathModel.CurrentSymbol == _turtlePathModel.Path[_turtlePathModel.CurrentPath].Count)
                    {
                        _turtlePathModel.CurrentSymbol = 0;
                        _turtlePathModel.CurrentPath++;
                        if (_turtlePathModel.CurrentPath == _turtlePathModel.Path.Count)
                            gameplayEventReceiver.Get<GameOverEvent>();
                        else
                        {
                            ref var updateTextEvent = ref turtlePathEntity.Get<UpdateTextEvent>();
                            updateTextEvent.Text = new string(_turtlePathModel.Path[_turtlePathModel.CurrentPath].ToArray());
                        }
                    }
                }
                else
                    gameplayEventReceiver.Get<WrongAnswerEvent>();
            }
        }
    }
}