using Pixelgrid;
using UnityEngine;

public class LeftDirectionState : IDirectionState
{
    public void RotateLeft(Turtle turtle)
    {
        turtle.SetDirectionState(new DownDirectionState());
    }

    public void RotateRight(Turtle turtle)
    {
        turtle.SetDirectionState(new UpDirectionState());
    }

    public void Move(Turtle turtle)
    {
        Vector3 startPos = turtle.transform.position;

        float posX = startPos.x;
        float posY = startPos.y;
        turtle.Position.Y--;
        posX -= turtle.offsetX;

        turtle.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }

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
