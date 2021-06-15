using Pixelgrid;
using UnityEngine;

public class DownDirectionState : IDirectionState
{
    public void RotateLeft(Turtle turtle)
    {
        turtle.SetDirectionState(new RightDirectionState());
    }

    public void RotateRight(Turtle turtle)
    {
        turtle.SetDirectionState(new LeftDirectionState());
    }

    public void Move(Turtle turtle)
    {
        Vector3 startPos = turtle.transform.position;

        float posX = startPos.x;
        float posY = startPos.y;
        turtle.Position.X++;
        posY -= turtle.offsetY;

        turtle.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }

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
