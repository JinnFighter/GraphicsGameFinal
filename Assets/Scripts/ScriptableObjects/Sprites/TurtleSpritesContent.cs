using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Sprites
{
    [CreateAssetMenu(fileName = "TurtleSpritesContent", menuName = "Content/TurtleSpritesContent")]
    public class TurtleSpritesContent : ScriptableObject
    {
        [SerializeField] private Sprite _turtleUp;
        [SerializeField] private Sprite _turtleDown;
        [SerializeField] private Sprite _turtleLeft;
        [SerializeField] private Sprite _turtleRight;
        
        public Sprite this[LookDirection direction] => direction switch
        {
            LookDirection.Up => _turtleUp,
            LookDirection.Down => _turtleDown,
            LookDirection.Left => _turtleLeft,
            LookDirection.Right => _turtleRight,
            _ => _turtleRight
        };
    }
}
