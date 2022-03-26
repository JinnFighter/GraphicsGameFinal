using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Sprites
{
    [CreateAssetMenu(fileName = "TurtleSpritesContent", menuName = "Content/TurtleSpritesContent")]
    public class TurtleSpritesContent : ScriptableObject
    {
        [field: SerializeField] public Sprite TurtleUp { get; private set; }
        [field: SerializeField] public Sprite TurtleDown { get; private set; }
        [field: SerializeField] public Sprite TurtleLeft { get; private set; }
        [field: SerializeField] public Sprite TurtleRight { get; private set; }
        
        public Sprite this[LookDirection direction] => direction switch
        {
            LookDirection.Up => TurtleUp,
            LookDirection.Down => TurtleDown,
            LookDirection.Left => TurtleLeft,
            LookDirection.Right => TurtleRight,
            _ => TurtleRight
        };
    }
}
