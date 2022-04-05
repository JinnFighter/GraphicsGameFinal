using System.Collections.Generic;

namespace Pixelgrid.DataModels
{
    public class TurtlePathModel
    {
        public readonly List<List<char>> Path = new List<List<char>>();
        public int CurrentPath;
        public int CurrentSymbol;
    }
}
