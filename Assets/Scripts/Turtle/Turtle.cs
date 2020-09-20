using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public Pixel originalPixel;
    private IDirectionState _directionState;

    public enum Direction { UP, LEFT, DOWN, RIGHT };

    public Direction Look { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    private readonly int angle = 90;
    private readonly float offsetX;
    private readonly float offsetY;

    public void SetDirectionState(IDirectionState directionState) => _directionState = directionState;

    public void RotateLeft()
    {
        this.gameObject.transform.Rotate(0, 0, angle);
        switch (Look)
        {
            case Direction.UP:
                Look = Direction.LEFT;
                break;
            case Direction.LEFT:
                Look = Direction.DOWN;
                break;
            case Direction.DOWN:
                Look = Direction.RIGHT;
                break;
            case Direction.RIGHT:
                Look = Direction.UP;
                break;
        }
    }

    public void RotateRight()
    {
        this.gameObject.transform.Rotate(0, 0, -angle);
        switch (Look)
        {
            case Direction.UP:
                Look = Direction.RIGHT;
                break;
            case Direction.RIGHT:
                Look = Direction.DOWN;
                break;
            case Direction.DOWN:
                Look = Direction.LEFT;
                break;
            case Direction.LEFT:
                Look = Direction.UP;
                break;
        }
    }

    public void MoveForward()
    {
        Vector3 startPos = this.transform.position;
        float posX = startPos.x;
        float posY = startPos.y;
        switch (Look)
        {
            case Direction.UP:
                X--;
                posY += offsetY;
            break;
            case Direction.RIGHT:
                Y++;
                posX += offsetX;
            break;
            case Direction.DOWN:
                X++;
                posY -= offsetY;
            break;
            case Direction.LEFT:
                Y--;
                posX -= offsetX;
            break;
        }
        this.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }
}