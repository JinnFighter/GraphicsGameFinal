using System.Collections.Generic;

namespace Pixelgrid.DataModels
{
    public class TurtlePathModel
    {
        public List<List<char>> Path = new List<List<char>>();
        public int CurrentPath;
        public int CurrentSymbol;

        public delegate void PathChanged(int pathIndex);

        public event PathChanged PathChangedEvent;
    }
}
