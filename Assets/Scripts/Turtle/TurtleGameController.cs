using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public TurtleGridPixelScript originalPixel;
    //private TurtleGridPixelScript[,] grid;
    [SerializeField] public Turtle turtle;
    [SerializeField] public InputField routeInputField;
    
    private string route;
    //public const int gridRows = 10;
    //public const int gridCols = 10;
    //private float offsetX;
    //private float offsetY;
    private int x;
    private int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    private enum commandsEnum { FORWARD, ROTATE_LEFT, ROTATE_RIGHT };
    private List<int> commands_history;
    private int look;
    private int cur_action;
    private bool finished;
    private int last_action;
    private Vector3 turtle_start_pos;
    private Quaternion turtle_start_rotation;
    // Start is called before the first frame update
    void Start()
    {
        route = "FFF-FF";
        commands_history = new List<int>();
        //grid = new TurtleGridPixelScript[gridRows, gridCols];
        Vector3 startPos = originalPixel.transform.position;
        //offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        //offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
        /* for(int i=0;i<numbers.Length;i++)
         {
             Debug.Log(numbers[i] + " ");
         }*/
        /*for (int i = 0; i < gridRows; i++)
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
        }*/
        routeInputField.text = route;
        x = 0;
        y = 0;
        look = (int)directionEnum.RIGHT;
        turtle.gameObject.transform.Rotate(0, 0, -90);
        turtle_start_pos = turtle.transform.position;
        turtle_start_rotation = turtle.transform.rotation;
        executeMoveSequence();
        cur_action = 0;
        last_action = -1;
        finished = false;
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
        //last_action = (int)commandsEnum.ROTATE_LEFT;
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
        //last_action = (int)commandsEnum.ROTATE_RIGHT;
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
                posY += GetComponent<GameField>().OffsetY;
                break;
            case (int)directionEnum.RIGHT:
                y++;
                posX += GetComponent<GameField>().OffsetX;
                break;
            case (int)directionEnum.DOWN:
                x++;
                posY -= GetComponent<GameField>().OffsetY;
                break;
            case (int)directionEnum.LEFT:
                y--;
                posX -= GetComponent<GameField>().OffsetX;
                break;
        }
        turtle.transform.position = new Vector3(posX, posY, startPos.z);
        //last_action = (int)commandsEnum.FORWARD;
    }
    void executeMoveSequence()
    {
        for (int i=0;i<route.Length;i++)
        {
           switch(route[i])
            {
                case 'F':
                    moveForward();
                    commands_history.Add((int)commandsEnum.FORWARD);
                    break;
                case '+':
                    rotateLeft();
                    commands_history.Add((int)commandsEnum.ROTATE_LEFT);
                    break;
                case '-':
                    rotateRight();
                    commands_history.Add((int)commandsEnum.ROTATE_RIGHT);
                    break;
            }
            //Debug.Log(route[i]);
        }
        turtle.transform.position = turtle_start_pos;
        turtle.transform.rotation = turtle_start_rotation;
        look = (int)directionEnum.RIGHT;
        x = 0;
        y = 0;
    }
    public void GameCheck(int action)
    {

        if(finished)
        {
            return;
        }
        last_action = action;
            if(last_action==commands_history[cur_action])
            {
                switch(last_action)
                {
                    case (int)commandsEnum.FORWARD:
                    //grid[x, y].setPixelState(true);
                    GetComponent<GameField>().grid[x, y].setPixelState(true);
                    moveForward();
                        break;
                    case (int)commandsEnum.ROTATE_LEFT:
                        rotateLeft();
                        break;
                    case (int)commandsEnum.ROTATE_RIGHT:
                        rotateRight();
                        break;
            }
                cur_action++;
                if(cur_action==commands_history.Count)
                {
                    finished = true;
                }
            }
        
    }
}
