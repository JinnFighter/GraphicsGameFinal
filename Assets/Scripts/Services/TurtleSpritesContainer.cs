using UnityEngine;

namespace Pixelgrid
{
    public class TurtleSpritesContainer : MonoBehaviour
    {
        public Sprite TurtleUp;
        public Sprite TurtleDown;
        public Sprite TurtleLeft;
        public Sprite TurtleRight;

        public Sprite GetSprite(LookDirection direction) => direction switch
        {
            LookDirection.Up => TurtleUp,
            LookDirection.Down => TurtleDown,
            LookDirection.Left => TurtleLeft,
            LookDirection.Right => TurtleRight,
            _ => TurtleRight
        };
    }
}
