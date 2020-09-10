using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleGameMode : GameMode
{
    private Pixel originalPixel;
    private Turtle turtle;
    private InputField routeInputField;

    private int pathsQuantity;
    private string[] paths;
    private int pathsLength;
    private string route;
    private int x;
    private int y;
    private enum directionEnum { UP, LEFT, DOWN, RIGHT };
    private enum commandsEnum { FORWARD, ROTATE_LEFT, ROTATE_RIGHT };
    private readonly Dictionary<int, char> commands = new Dictionary<int, char>
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
    private GameField _gameField;

    public TurtleGameMode(Pixel pixel, Turtle turtle, InputField inputField, GameplayTimer timer, GameField field, int difficulty) : base(difficulty)
    {
        originalPixel = pixel;
        this.turtle = turtle;
        routeInputField = inputField;
        _gameField = field;
        difficulty = _gameField.Difficulty;
        switch (difficulty)
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
        route = "";
        commands_history = new List<int>[pathsQuantity];
        for (var i = 0; i < pathsQuantity; i++)
            commands_history[i] = new List<int>();

        look = (int)directionEnum.RIGHT;
        turtle.gameObject.transform.Rotate(0, 0, -90);
        turtle.transform.position = new Vector3(_gameField.grid[x, y].transform.position.x, _gameField.grid[x, y].transform.position.y, turtle.transform.position.z);
        turtle_start_pos = turtle.transform.position;
        turtle_start_rotation = turtle.transform.rotation;
        cur_action = 0;
        last_action = -1;
        finished = false;
        generateStringPaths();

        Vector3 startPos = turtle_start_pos;
        routeInputField.text = paths[iteration];
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    ~TurtleGameMode()
    {
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }

    public void generateStringPaths()
    {
        for (var i = 0; i < pathsQuantity; i++)
        {
            iteration = i;
            for (var j = 0; j < pathsLength; j++)
            {
                var c = commands[UnityEngine.Random.Range(0, 2)];
                while (j == 0 && c != commands[0])
                    c = commands[UnityEngine.Random.Range(0, 2)];

                route = String.Concat(route, c);
            }
            paths[i] = route;
            executeMoveSequence();
            route = "";
        }
        iteration = 0;
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
    }

    public void moveForward()
    {
        var allowMove = true;
        var startPos = turtle.transform.position;
        var posX = startPos.x;
        var posY = startPos.y;
        switch (look)
        {
            case (int)directionEnum.UP:
                if (x == 0)
                {
                    allowMove = false;
                    break;
                }
                x--;
                posY += _gameField.OffsetY;
                break;
            case (int)directionEnum.RIGHT:
                if (y == _gameField.Width - 1)
                {
                    allowMove = false;
                    break;
                }
                y++;
                posX += _gameField.OffsetX;
                break;
            case (int)directionEnum.DOWN:
                if (x == _gameField.Height - 1)
                {
                    allowMove = false;
                    break;
                }
                x++;
                posY -= _gameField.OffsetY;
                break;
            case (int)directionEnum.LEFT:
                if (y == 0)
                {
                    allowMove = false;
                    break;
                }
                y--;
                posX -= _gameField.OffsetX;
                break;
        }
        if (allowMove)
            turtle.transform.position = new Vector3(posX, posY, startPos.z);
    }

    void executeMoveSequence()
    {
        for (var i = 0; i < route.Length; i++)
        {
            switch (route[i])
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
        }
        turtle.transform.position = turtle_start_pos;
        turtle.transform.rotation = turtle_start_rotation;
        look = (int)directionEnum.RIGHT;
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
    }

    public override void ChangeGameState()
    {
        if (!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
            eventReactor.OnChangeState(difficulty);
        }
        else
            gameActive = false;
    }

    public override void CheckAction(Pixel invoker)
    {
        if (!gameActive) return;

        //if (!timer.Counting) return;

        if (finished) return;

        //last_action = action;
        if (last_action == commands_history[iteration][cur_action])
        {
            Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
            switch (last_action)
            {
                case (int)commandsEnum.FORWARD:
                    _gameField.grid[x, y].setPixelState(true);
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
                cur_action = 0;
                iteration++;
                if (iteration == pathsQuantity)
                {
                    eventReactor.OnGameOver();
                }
                else
                    routeInputField.text = paths[iteration];
            }
        }
        else
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.ClearGrid();
        route = "";
        for (var i = 0; i < pathsQuantity; i++)
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
        eventReactor.OnRestart();

        Messenger.Broadcast(GameEvents.START_GAME);
    }
}
