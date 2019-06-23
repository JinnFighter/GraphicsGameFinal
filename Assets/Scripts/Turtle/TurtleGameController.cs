using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public GridPixelScript originalPixel;
    //private TurtleGridPixelScript[,] grid;
    [SerializeField] public Turtle turtle;
    [SerializeField] public InputField routeInputField;

    private int pathsQuantity;
    private string[] paths;
    private int pathsLength;
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
    private bool gameActive;
    private bool gameStarted;
    private int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = GetComponent<GameField>().Difficulty;
        switch(difficulty)
        {
            case 0:
                pathsQuantity = 5;
                pathsLength = 5;
                x = 4;
                y = 4;
                break;
            case 1:
                pathsQuantity = 7;
                pathsLength = 7;
                x = 6;
                y = 6;
                break;
            case 2:
                pathsQuantity = 10;
                pathsLength = 10;
                x = 7;
                y = 7;
                break;
            default:
                pathsQuantity = 5;
                pathsLength = 5;
                x = 4;
                y = 4;
                break;
        }
        paths = new string[pathsQuantity];
        //route = "FFF-FF";
        route = "";
        commands_history = new List<int>[pathsQuantity];
        for (int i = 0; i < pathsQuantity; i++)
        {
            commands_history[i] = new List<int>();
        }
        
        look = (int)directionEnum.RIGHT;
        turtle.gameObject.transform.Rotate(0, 0, -90);
        turtle.transform.position = new Vector3(GetComponent<GameField>().grid[x, y].transform.position.x, GetComponent<GameField>().grid[x, y].transform.position.y, turtle.transform.position.z);
        turtle_start_pos = turtle.transform.position;
        turtle_start_rotation = turtle.transform.rotation;
        // executeMoveSequence();
        cur_action = 0;
        last_action = -1;
        finished = false;
        generateStringPaths();

        //Vector3 startPos = originalPixel.transform.position;
        Vector3 startPos = turtle_start_pos;
        routeInputField.text = paths[iteration];
        //routeInputField.text = route;
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.AddListener(GameEvents.RESTART_GAME, RestartGame);
        GetComponent<GameplayTimer>().Format = GameplayTimer.TimerFormat.smms;
        if (PlayerPrefs.GetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit") == 0)
        {
            Messenger.Broadcast(GameEvents.START_GAME);
        }
        //Messenger.Broadcast(GameEvents.START_GAME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, RestartGame);
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
        switch(difficulty)
        {
            case 0:
                x = 4;
                y = 4;
                break;
            case 1:
                x = 6;
                y = 6;
                break;
            case 2:
                x = 7;
                y = 7;
                break;
            default:
                x = 4;
                y = 4;
                break;
        }
        
    }
    public void GameCheck(int action)
    {
        if (!gameActive)
        {
            return;
        }
        if (!GetComponent<GameplayTimer>().Counting)
        {
            Debug.Log("Not Counting due to finish or no start");
            return;
        }
        if (finished)
        {
            return;
        }
        last_action = action;
        if(last_action==commands_history[iteration][cur_action])
        {
            Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER,100);
            switch (last_action)
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
            if (cur_action == commands_history[iteration].Count)
            {
                //finished = true;
                cur_action = 0;
                iteration++;
                if (iteration == pathsQuantity)
                {
                    GetComponent<GameplayTimer>().StopTimer();
                    Messenger.Broadcast(GameEvents.GAME_OVER);
                }
                else
                {
                    routeInputField.text = paths[iteration];
                }
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
            for(int j=0;j<pathsLength;j++)
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
    public void PauseGame()
    {
        if(gameStarted)
        {
            gameActive = false;
            GetComponent<GameplayTimer>().PauseTimer();
        }
        
    }
    public void ContinueGame()
    {
        if(gameStarted)
        {
            gameActive = true;
            GetComponent<GameplayTimer>().ResumeTimer();
        }
        
    }
    public void ChangeGameState()
    {
        if (!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
            switch (difficulty)
            {
                case 0:
                    GetComponent<GameplayTimer>().StartTime = 60f;
                    break;
                case 1:
                    GetComponent<GameplayTimer>().StartTime = 80f;
                    break;
                case 2:
                    GetComponent<GameplayTimer>().StartTime = 120f;
                    break;
                default:
                    GetComponent<GameplayTimer>().StartTime = 60f;
                    break;
            }
            GetComponent<GameplayTimer>().StartTimer();
        }
        else
        {
            gameActive = false;
        }
    }
    public void RestartGame()
    {
        gameActive = false;
        gameStarted = false;
        GetComponent<GameField>().clearGrid();
        route = "";
        for (int i=0;i<pathsQuantity;i++)
        {
            commands_history[i].Clear();
            paths[i] = "";
        }
        switch (difficulty)
        {
            case 0:
                x = 4;
                y = 4;
                break;
            case 1:
                x = 6;
                y = 6;
                break;
            case 2:
                x = 7;
                y = 7;
                break;
            default:
                x = 4;
                y = 4;
                break;
        }
        cur_action = 0;
        last_action = -1;
        finished = false;
        iteration = 0;
        look = (int)directionEnum.RIGHT;
        generateStringPaths();
        switch (difficulty)
        {
            case 0:
                x = 4;
                y = 4;
                break;
            case 1:
                x = 6;
                y = 6;
                break;
            case 2:
                x = 7;
                y = 7;
                break;
            default:
                x = 4;
                y = 4;
                break;
        }
        iteration = 0;
        cur_action = 0;
        last_action = -1;
        finished = false;
        look = (int)directionEnum.RIGHT;
        turtle.transform.position = turtle_start_pos;
        turtle.transform.rotation = turtle_start_rotation;
        routeInputField.text = paths[iteration];
        GetComponent<GameplayTimer>().timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
    public void SendStartGameEvent()
    {
        if (PlayerPrefs.GetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit") == 1)
        {
            PlayerPrefs.SetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit", 0);
            PlayerPrefs.Save();
            Messenger.Broadcast(GameEvents.START_GAME);
        }

    }
}
