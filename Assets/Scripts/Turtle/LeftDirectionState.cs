using Pixelgrid;
using UnityEngine;

public class LeftDirectionState : IDirectionState
{
    public Vector2Int Move(Vector2Int position) => new Vector2Int(position.x, position.y - 1);

    public IDirectionState RotateLeft(out LookDirection lookDirection) 
    {
        lookDirection = LookDirection.Down;
        return new DownDirectionState();
    }

    public IDirectionState RotateRight(out LookDirection lookDirection)
    {
        lookDirection = LookDirection.Up;
        return new UpDirectionState();
    }
}
