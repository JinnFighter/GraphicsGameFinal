using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public GridPixelScript originalPixel;
    //private TurtleGridPixelScript[,] grid;
    [SerializeField] public Turtle turtle;
    [SerializeField] public InputField routeInputField;
    private int pathsQuantity;
    private string[] paths;
    private string route;
    //public const int gridRows = 10;
    //public const int gridCols = 10;
    //private float offsetX;
    //private float offsetY;
    private int x;
    private int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    private enum commandsEnum { FORWARD, ROTATE_LEFT, ROTATE_RIGHT };
    private Dictionary<int, char> commands = new Dictionary<int, char>
    {
        {(int)commandsEnum.FORWARD, 'F' },
        {(int)commandsEnum.ROTATE_LEFT, '+' },
        {(int)commandsEnum.ROTATE_RIGHT, '-' }
    };
    private List<int>[] commands_history;
    private int look;
    private int cur_action;
    private bool finished;
    private int last_action;
    private int iteration;
    private Vector3 turtle_start_pos;
    private Quaternion turtle_start_rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        pathsQuantity = 5;
        paths = new string[pathsQuantity];
        //route = "FFF-FF";
        route = "";
        commands_history = new List<int>[pathsQuantity];
        for (int i = 0; i < pathsQuantity; i++)
        {
            commands_history[i] = new List<int>();
        }
        x = 0;
        y = 0;
        look = (int)directionEnum.RIGHT;
        turtle.gameObject.transform.Rotate(0, 0, -90);
        turtle_start_pos = turtle.transform.position;
        turtle_start_rotation = turtle.transform.rotation;
        // executeMoveSequence();
        cur_action = 0;
        last_action = -1;
        finished = false;
        generateStringPaths();
       
        //grid = new TurtleGridPixelScript[gridRows, gridCols];
        Vector3 startPos = originalPixel.transform.position;
        routeInputField.text = paths[iteration];
        //routeInputField.text = route;
       
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
        bool allowMove = true;
        Vector3 startPos = turtle.transform.position;
        float posX = startPos.x;
        float posY = startPos.y;
        switch (look)
        {
            case (int)directionEnum.UP:
                if(x==0)
                {
                    allowMove = false;
                    break;
                }
                x--;
                posY += GetComponent<GameField>().OffsetY;
                break;
            case (int)directionEnum.RIGHT:
                if (y == GetComponent<GameField>().GridCols-1)
                {
                    allowMove = false;
                    break;
                }
                y++;
                posX += GetComponent<GameField>().OffsetX;
                break;
            case (int)directionEnum.DOWN:
                if (x == GetComponent<GameField>().GridRows - 1)
                {
                    allowMove = false;
                    break;
                }
                x++;
                posY -= GetComponent<GameField>().OffsetY;
                break;
            case (int)directionEnum.LEFT:
                if (y == 0)
                {
                    allowMove = false;
                    break;
                }
                y--;
                posX -= GetComponent<GameField>().OffsetX;
                break;
        }
        if(allowMove)
        {
            turtle.transform.position = new Vector3(posX, posY, startPos.z);
        }
        
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
                    commands_history[iteration].Add((int)commandsEnum.FORWARD);
                    break;
                case '+':
                    rotateLeft();
                    commands_history[iteration].Add((int)commandsEnum.ROTATE_LEFT);
                    break;
                case '-':
                    rotateRight();
                    commands_history[iteration].Add((int)commandsEnum.ROTATE_RIGHT);
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
            if(last_action==commands_history[iteration][cur_action])
            {
            Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER,100);
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
                if(cur_action==commands_history[iteration].Count)
                {
                    //finished = true;
                    cur_action = 0;
                    iteration++;
                    routeInputField.text = paths[iteration];
                }
            }
        else
        {
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
    }
    public void generateStringPaths()
    {
        for(int i=0;i<pathsQuantity;i++)
        {
            iteration = i;
            for(int j=0;j<5;j++)
            {
                
                char c = commands[UnityEngine.Random.Range(0, 2)];
                while (j == 0 && c != commands[0])
                {
                   c = commands[UnityEngine.Random.Range(0, 2)];
                }
                //String.Concat(route, c);
                route = String.Concat(route, c);
            }
            paths[i] = route;
            executeMoveSequence();
            route = "";
        }
        iteration = 0;
    }
}
