using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public Pixel originalPixel;

    private enum directionEnum { UP, LEFT, DOWN, RIGHT };

    public int Look { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    private readonly int angle = 90;
    private readonly float offsetX;
    private readonly float offsetY;

    public void rotateLeft()
    {
        this.gameObject.transform.Rotate(0, 0, angle);
        switch (Look)
        {
            case (int)directionEnum.UP:
                Look = (int)directionEnum.LEFT;
                break;
            case (int)directionEnum.LEFT:
                Look = (int)directionEnum.DOWN;
                break;
            case (int)directionEnum.DOWN:
                Look = (int)directionEnum.RIGHT;
                break;
            case (int)directionEnum.RIGHT:
                Look = (int)directionEnum.UP;
                break;
        }
    }

    public void rotateRight()
    {
        this.gameObject.transform.Rotate(0, 0, -angle);
        switch (Look)
        {
            case (int)directionEnum.UP:
                Look = (int)directionEnum.RIGHT;
                break;
            case (int)directionEnum.RIGHT:
                Look = (int)directionEnum.DOWN;
                break;
            case (int)directionEnum.DOWN:
                Look = (int)directionEnum.LEFT;
                break;
            case (int)directionEnum.LEFT:
                Look = (int)directionEnum.UP;
                break;
        }
    }

    public void moveForward()
    {
        Vector3 startPos = this.transform.position;
        float posX = startPos.x;
        float posY = startPos.y;
        switch (Look)
        {
            case (int)directionEnum.UP:
                X--;
                posY += offsetY;
            break;
            case (int)directionEnum.RIGHT:
                Y++;
                posX += offsetX;
            break;
            case (int)directionEnum.DOWN:
                X++;
                posY -= offsetY;
            break;
            case (int)directionEnum.LEFT:
                Y--;
                posX -= offsetX;
            break;
        }
        this.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }
}