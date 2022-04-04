using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.UI.Presenters;
using Pixelgrid.UI.Views;

namespace Pixelgrid.Systems.GameModes.Turtle 
{
    public sealed class CreateTurtlePathSystem : IEcsInitSystem 
    {
        private readonly TurtlePathModel _turtlePathModel = null;
        private readonly TurtlePathView _turtlePathView = null;

        public void Init() 
        {
            _turtlePathModel.Path = new List<List<char>>();
            var presenter = new TurtlePathPresenter(_turtlePathModel, _turtlePathView);
        }
    }
}