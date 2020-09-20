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
}
