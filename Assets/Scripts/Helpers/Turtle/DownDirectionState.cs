using Pixelgrid;
using UnityEngine;

public class DownDirectionState : IDirectionState
{
    public Vector2Int Move(Vector2Int position) => new Vector2Int(position.x + 1, position.y);

    public IDirectionState RotateLeft(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Right;
        return new RightDirectionState();
    }

    public IDirectionState RotateRight(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Left;
        return new LeftDirectionState();
    }
}
