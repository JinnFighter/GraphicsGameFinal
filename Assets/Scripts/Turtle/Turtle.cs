using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public GridPixelScript originalPixel;
    private int x;
    private int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    private int look;
    public int Look
    {
        get { return look; }
        set { look = value; }
    }
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }
    private int angle = 90;
    private float offsetX;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rotateLeft()
    {
        this.gameObject.transform.Rotate(0,0, 90);
        switch (look)
        {
            case (int)directionEnum.UP:
                look = (int)directionEnum.LEFT;
                break;
            case (int)directionEnum.LEFT:
                look = (int)directionEnum.DOWN;
                break;
            case (int)directionEnum.DOWN:
                look = (int)directionEnum.RIGHT;
                break;
            case (int)directionEnum.RIGHT:
                look = (int)directionEnum.UP;
                break;
        }
    }
    public void rotateRight()
    {
        this.gameObject.transform.Rotate(0, 0, -90);
        switch (look)
        {
            case (int)directionEnum.UP:
                look = (int)directionEnum.RIGHT;
                break;
            case (int)directionEnum.RIGHT:
                look = (int)directionEnum.DOWN;
                break;
            case (int)directionEnum.DOWN:
                look = (int)directionEnum.LEFT;
                break;
            case (int)directionEnum.LEFT:
                look = (int)directionEnum.UP;
                break;
        }
    }
    public void moveForward()
    {
        Vector3 startPos = this.transform.position;
        float posX = startPos.x;
        float posY = startPos.y;
        switch (look)
        {
            case (int)directionEnum.UP:
                x--;
                posY += offsetY;
            break;
            case (int)directionEnum.RIGHT:
                y++;
                posX += offsetX;
            break;
            case (int)directionEnum.DOWN:
                x++;
                posY -= offsetY;
            break;
            case (int)directionEnum.LEFT:
                y--;
                posX -= offsetX;
            break;
        }
        this.gameObject.transform.position = new Vector3(posX, posY, startPos.z);
    }
}
