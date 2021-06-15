using Pixelgrid;
using UnityEngine;

public interface IDirectionState
{
    Vector2Int Move(Vector2Int position);
    IDirectionState RotateLeft(out LookDirection lookDirection);
    IDirectionState RotateRight(out LookDirection lookDirection);
}
