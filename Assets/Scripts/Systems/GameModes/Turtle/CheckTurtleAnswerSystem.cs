using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CheckTurtleAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<TurtleComponent, TurtlePath, PixelPosition, TurtleCommand> _filter = null;

        void IEcsRunSystem.Run()
        { 
            foreach(var index in _filter)
            {
                var turtlePathEntity = _filter.GetEntity(index);
                ref var turtlePath = ref _filter.Get2(index);
                var path = turtlePath.Path;
                var gameplayEventReceiver = _filter.GetEntity(index);

                var turtleCommand = _filter.Get4(index);
                var symbol = turtleCommand.CommandSymbol;
                if(symbol == path[turtlePath.CurrentPath][turtlePath.CurrentSymbol])
                {
                    gameplayEventReceiver.Get<CorrectAnswerEvent>();
                    turtlePath.CurrentSymbol++;
                    if(turtlePath.CurrentSymbol == turtlePath.Path[turtlePath.CurrentPath].Count)
                    {
                        turtlePath.CurrentSymbol = 0;
                        turtlePath.CurrentPath++;
                        if (turtlePath.CurrentPath == turtlePath.Path.Count)
                            gameplayEventReceiver.Get<GameOverEvent>();
                        else
                        {
                            ref var updateTextEvent = ref turtlePathEntity.Get<UpdateTextEvent>();
                            updateTextEvent.Text = new string(turtlePath.Path[turtlePath.CurrentPath].ToArray());
                        }
                    }
                }
                else
                    gameplayEventReceiver.Get<WrongAnswerEvent>();
            }
        }
    }
}