using System.Collections.Generic;

namespace Pixelgrid.DataModels
{
    public class TurtlePathModel
    {
        public List<List<char>> Path = new List<List<char>>();
        private int _currentPath;
        public int CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                PathChangedEvent?.Invoke(_currentPath);
            }
        }
        public int CurrentSymbol;

        public delegate void PathChanged(int pathIndex);

        public event PathChanged PathChangedEvent;
    }
}
