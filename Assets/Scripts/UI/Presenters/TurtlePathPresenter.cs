using Pixelgrid.DataModels;
using Pixelgrid.UI.Views;
using UnityEngine;

namespace Pixelgrid.UI.Presenters
{
    public class TurtlePathPresenter : MonoBehaviour
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
            _turtlePathView.SetText(new string(_turtlePathModel.Path[_turtlePathModel.CurrentPath].ToArray()));
        }
    }
}
