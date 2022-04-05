using Leopotam.Ecs;

namespace Pixelgrid.Components.UI.Turtle
{
    public struct TurtlePathViewModel : IEcsAutoReset<TurtlePathViewModel>
    {
        public int PathIndex;
        
        public void AutoReset(ref TurtlePathViewModel c)
        {
            c.PathIndex = -1;
        }
    }
}
