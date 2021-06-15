using Pixelgrid;
using UnityEngine;

public class UpDirectionState : IDirectionState
{
    public Vector2Int Move(Vector2Int position) => new Vector2Int(position.x - 1, position.y);

    public IDirectionState RotateLeft(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Left;
        return new LeftDirectionState();
    }

    public IDirectionState RotateRight(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Right;
        return new RightDirectionState();
    }
}
