using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public Pixel originalPixel;

    private enum Directions { UP, LEFT, DOWN, RIGHT };

    public int Look { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    private readonly int angle = 90;
    private readonly float offsetX;
    private readonly float offsetY;

    public void RotateLeft()
    {
        this.gameObject.transform.Rotate(0, 0, angle);
        switch (Look)
        {
            case (int)Directions.UP:
                Look = (int)Directions.LEFT;
                break;
            case (int)Directions.LEFT:
                Look = (int)Directions.DOWN;
                break;
            case (int)Directions.DOWN:
                Look = (int)Directions.RIGHT;
                break;
            case (int)Directions.RIGHT:
                Look = (int)Directions.UP;
                break;
        }
    }

    public void RotateRight()
    {
        this.gameObject.transform.Rotate(0, 0, -angle);
        switch (Look)
        {
            case (int)Directions.UP:
                Look = (int)Directions.RIGHT;
                break;
            case (int)Directions.RIGHT:
                Look = (int)Directions.DOWN;
                break;
            case (int)Directions.DOWN:
                Look = (int)Directions.LEFT;
                break;
            case (int)Directions.LEFT:
                Look = (int)Directions.UP;
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
            case (int)Directions.UP:
                X--;
                posY += offsetY;
            break;
            case (int)Directions.RIGHT:
                Y++;
                posX += offsetX;
            break;
            case (int)Directions.DOWN:
                X++;
                posY -= offsetY;
            break;
            case (int)Directions.LEFT:
                Y--;
                posX -= offsetX;
            break;
        }
        this.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }
}