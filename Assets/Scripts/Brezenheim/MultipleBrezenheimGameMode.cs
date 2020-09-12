using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : GameMode
{
    private Pixel[,] lines;
    private int linesQuantity;
    private List<Pixel>[] linePoints;
    private List<int>[] ds;
    private Pixel last_point;
    private Pixel prev_point;
    private int cur_line;
    private int iteration;
    private int minLineLength;
    private int maxLineLength;
    private InputField textField;
    private GameField _gameField;

    public MultipleBrezenheimGameMode(GameplayTimer timer, int diff, GameField inputField, InputField nextTextField) : base(diff)
    {
        textField = nextTextField;
        _gameField = inputField;
        difficulty = diff;
        gameActive = false;
        gameStarted = false;
        switch (difficulty)
        {
            case 0:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                break;
            case 1:
                linesQuantity = 7;
                minLineLength = 4;
                maxLineLength = 8;
                break;
            case 2:
                linesQuantity = 10;
                minLineLength = 5;
                maxLineLength = 10;
                break;
            default:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                break;
        }
        lines = new Pixel[2, linesQuantity];
        linePoints = new List<Pixel>[linesQuantity];
        ds = new List<int>[linesQuantity];
        for (var i = 0; i < linesQuantity; i++)
        {
            linePoints[i] = new List<Pixel>();
            ds[i] = new List<int>();
        }
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);
        GeneratePolygon();

        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        textField.text = ds[0][0].ToString();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }
    
    ~MultipleBrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
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

            if (cur_line == linesQuantity)
            {
                Messenger.Broadcast(GameEvents.TIMER_STOP);
                return;
            }

            if (prev_point == last_point)
            {
                cur_line++;
                if (cur_line == linesQuantity)
                {
                    eventReactor.OnGameOver();
                    return;
                }
                else
                {
                    iteration = 0;
                    _gameField.ClearGrid();
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    lines[0, cur_line].setPixelState(true);
                    lines[1, cur_line].setPixelState(true);
                    last_point = linePoints[cur_line][linePoints[cur_line].Count - 1];
                    prev_point = null;
                    textField.text = ds[cur_line][iteration].ToString();
                }
            }
            else
            {
                prev_point = linePoints[cur_line][iteration];

                if (invoker == prev_point)
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    invoker.setPixelState(true);
                    iteration++;
                    textField.text = ds[cur_line][iteration].ToString();
                    prev_point = linePoints[cur_line][iteration];
                }
                else
                    Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            }
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.ClearGrid();
        for (var i = 0; i < linesQuantity; i++)
        {
            ds[i].Clear();
            linePoints[i].Clear();
        }
        cur_line = 0;
        iteration = 0;

        GeneratePolygon();
        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public void GeneratePolygon()
    {
        var geometry = new Geometry();
        for (var i = 0; i < linesQuantity; i++)
        {
            int x0;
            int y0;

            int x1;
            int y1;

            if (i == 0)
            {
                x0 = UnityEngine.Random.Range(0, 9);
                y0 = UnityEngine.Random.Range(0, 9);

                x1 = UnityEngine.Random.Range(0, 9);
                y1 = UnityEngine.Random.Range(0, 9);

                var lineLength = geometry.GetLineLength(x0, y0, x1, y1);
                while (lineLength > maxLineLength
                    || lineLength < minLineLength)
                {
                    x1 = UnityEngine.Random.Range(0, 9);
                    y1 = UnityEngine.Random.Range(0, 9);
                    lineLength = geometry.GetLineLength(x0, y0, x1, y1);
                }
            }
            else
            {
                x0 = lines[1, i - 1].Y;
                y0 = lines[1, i - 1].X;
                if (i == linesQuantity - 1)
                {
                    x1 = lines[0, 0].Y;
                    y1 = lines[0, 0].X;

                    while (true)
                    {
                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (geometry.HasSegmentsIntersection(lines[0, j], lines[1, j],
                                _gameField.grid[y0, x0], _gameField.grid[y1, x1]))
                            {
                                break;
                            }
                        }
                        if (check)
                            continue;
                        else
                            break;
                    }
                }
                else
                {
                    while (true)
                    {
                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);
                        var lineLength = geometry.GetLineLength(x0, y0, x1, y1);
                        if (lineLength > maxLineLength
                        || lineLength <= minLineLength - 1)
                        {
                            lineLength = geometry.GetLineLength(x0, y0, x1, y1);
                            continue;
                        }

                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (geometry.HasSegmentsIntersection(lines[0, j], linePoints[j][linePoints[j].Count - 2],
                             _gameField.grid[y0, x0], _gameField.grid[y1, x1]))
                            {
                                break;
                            }
                        }

                        if (check)
                            continue;
                        else
                            break;
                    }
                }
            }

            lines[0, i] = _gameField.grid[y0, x0];
            lines[1, i] = _gameField.grid[y1, x1];
            Algorithms.GetBrezenheimLineData(_gameField, x0, y0, x1, y1, out ds[i], out linePoints[i]);
        }
    }
}
