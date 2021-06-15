using UnityEngine;

public interface IDirectionState
{
    void RotateLeft(Turtle turtle);
    void RotateRight(Turtle turtle);
    void Move(Turtle turtle);
    Vector2Int Move(Vector2Int position);
    IDirectionState RotateLeft();
    IDirectionState RotateRight();
}
