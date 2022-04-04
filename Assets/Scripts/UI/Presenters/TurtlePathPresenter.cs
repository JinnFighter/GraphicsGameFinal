using Pixelgrid.DataModels;
using Pixelgrid.UI.Views;

namespace Pixelgrid.UI.Presenters
{
    public class TurtlePathPresenter
    {
        private readonly TurtlePathModel _turtlePathModel;
        private readonly TurtlePathView _turtlePathView;

        public TurtlePathPresenter(TurtlePathModel turtlePathModel, TurtlePathView turtlePathView)
        {
            _turtlePathModel = turtlePathModel;
            _turtlePathView = turtlePathView;
            
            _turtlePathModel.PathChangedEvent += OnPathChangedEvent;
        }

        private void OnPathChangedEvent(int pathIndex)
        {
            if (pathIndex < _turtlePathModel.Path.Count)
            {
                _turtlePathView.SetText(new string(_turtlePathModel.Path[_turtlePathModel.CurrentPath].ToArray()));
            }
        }
    }
}
