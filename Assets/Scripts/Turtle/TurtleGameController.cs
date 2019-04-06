using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public TurtleGridPixelScript originalPixel;
    private TurtleGridPixelScript[,] grid;
    [SerializeField] public Turtle turtle;
    [SerializeField] public InputField routeInputField;
    
    private string route;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;
    private int x;
    private int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    private int look;
    // Start is called before the first frame update
    void Start()
    {
        route = "FFF-FF";
        grid = new TurtleGridPixelScript[gridRows, gridCols];
        Vector3 startPos = originalPixel.transform.position;

        offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
        /* for(int i=0;i<numbers.Length;i++)
         {
             Debug.Log(numbers[i] + " ");
         }*/
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
            {
                TurtleGridPixelScript pixel;

                if (i == 0 && j == 0)
                {
                    pixel = originalPixel;
                    //grid[i,j] = originalPixel;
                }
                else
                {
                    //grid[i,j] = Instantiate(originalPixel) as GridPixelScript;
                    pixel = Instantiate(originalPixel) as TurtleGridPixelScript;
                    float posX = (offsetX * j) + startPos.x;
                    float posY = -(offsetY * i) + startPos.y;
                    pixel.transform.position = new Vector3(posX, posY, startPos.z);
                }
                grid[i, j] = pixel;
            }
        }
        routeInputField.text = route;
        x = 0;
        y = 0;
        look = (int)directionEnum.RIGHT;
        turtle.gameObject.transform.Rotate(0, 0, -90);
        executeMoveSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rotateLeft()
    {
        turtle.transform.Rotate(0, 0, 90);
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
        turtle.transform.Rotate(0, 0, -90);
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
        //Debug.Log(look);
    }
    public void moveForward()
    {
        Vector3 startPos = turtle.transform.position;
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
        Debug.Log(x.ToString() + " " + y.ToString());
        turtle.transform.position = new Vector3(posX, posY, startPos.z);
    }
    void executeMoveSequence()
    {
        Debug.Log("LooksNowStart" + look);
        for (int i=0;i<route.Length;i++)
        {
           switch(route[i])
            {
                case 'F':
                    grid[x,y].setPixelState(true);
                    moveForward();
                    Debug.Log("Moved"+look);
                    break;
                case '+':
                    rotateLeft();
                    Debug.Log("RotatedLeft"+look);
                    break;
                case '-':
                    rotateRight();
                    Debug.Log("RotatedRight"+look);
                    break;
            }
            //Debug.Log(route[i]);
        }
    }
}
