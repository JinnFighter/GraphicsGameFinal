using UnityEngine;

public class UpDirectionState : IDirectionState
{
    public void RotateLeft(Turtle turtle)
    {
        turtle.SetDirectionState(new LeftDirectionState());
    }

    public void RotateRight(Turtle turtle)
    {
        turtle.SetDirectionState(new RightDirectionState());
    }

    public void Move(Turtle turtle)
    {
        Vector3 startPos = turtle.transform.position;

        float posX = startPos.x;
        float posY = startPos.y;
        turtle.Position.X--;
        posY += turtle.offsetY;

        turtle.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }
}
