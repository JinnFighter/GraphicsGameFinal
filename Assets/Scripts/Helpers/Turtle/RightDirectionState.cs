using Pixelgrid;
using UnityEngine;

public class RightDirectionState : IDirectionState
{
    public Vector2Int Move(Vector2Int position) => new Vector2Int(position.x, position.y + 1);

    public IDirectionState RotateLeft(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Up;
        return new UpDirectionState();
    }

    public IDirectionState RotateRight(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Down;
        return new DownDirectionState();
    }
}
