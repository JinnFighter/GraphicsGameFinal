using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public TurtleGridPixelScript originalPixel;
    public int x;
    public int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    public int look;
    private int angle = 90;
    private float offsetX;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        y = 0;
        offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
        this.gameObject.transform.Rotate(0, 0, -90);
       look = (int)directionEnum.RIGHT;
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
        Vector3 startPos = this.gameObject.transform.position;
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
