using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CheckTurtleAnswerSystem : IEcsRunSystem 
    {
        private EcsFilter<GameplayEventReceiver, TurtleCommand> _filter;
        private EcsFilter<TurtlePath> _turtlePathFilter;
        private EcsFilter<TurtleComponent, PixelPosition> _turtleFilter;

        void IEcsRunSystem.Run()
        { 
            foreach(var index in _filter)
            {
                ref var turtlePath = ref _turtlePathFilter.Get1(0);
                var turtlePathEntity = _turtlePathFilter.GetEntity(0);
                var path = turtlePath.Path;
                var gameplayEventReceiver = _filter.GetEntity(index);

                var turtleCommand = _filter.Get2(index);
                var symbol = turtleCommand.CommandSymbol;
                var turtleEntity = _turtleFilter.GetEntity(0);
                ref var turtle = ref turtleEntity.Get<TurtleComponent>();
                ref var turtlePosition = ref turtleEntity.Get<PixelPosition>();
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