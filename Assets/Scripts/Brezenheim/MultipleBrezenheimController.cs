using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class MultipleBrezenheimController : MonoBehaviour
{
    private GridPixelScript[,] lines;
    private int linesQuantity;
    private List<GridPixelScript>[] linePoints;
    private List<int>[] ds;
    private GridPixelScript last_point;
    private GridPixelScript prev_point;
    private int cur_line;
    private int iteration;
    private bool gameActive;
    private bool gameStarted;
    private int minLineLength;
    private int maxLineLength;
    private int maxLengthSum;
    private int difficulty;
    [SerializeField] private InputField textField;
    
    // Start is called before the first frame update
    void Start()
    {
        difficulty = GetComponent<GameField>().Difficulty;
        gameActive = false;
        gameStarted = false;
        switch (difficulty)
        {
            case 0:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                maxLengthSum = 20;
                break;
            case 1:
                linesQuantity = 7;
                minLineLength = 4;
                maxLineLength = 8;
                maxLengthSum = 48;
                break;
            case 2:
                linesQuantity = 10;
                minLineLength = 5;
                maxLineLength = 10;
                maxLengthSum = 90;
                break;
            default:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                maxLengthSum = 20;
                break;
        }
        lines = new GridPixelScript[2, linesQuantity];
        linePoints = new List<GridPixelScript>[linesQuantity];
        ds = new List<int>[linesQuantity];
        for (int i = 0; i < linesQuantity; i++)
        {
            linePoints[i] = new List<GridPixelScript>();
            ds[i] = new List<int>();
        }
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.AddListener(GameEvents.RESTART_GAME, RestartGame);
        GeneratePolygon();

        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        textField.text = ds[0][0].ToString();
        GameplayTimer timer = GetComponent<GameplayTimer>();
        timer.Format = GameplayTimer.TimerFormat.smms;
        timer.timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Messenger<GridPixelScript>.RemoveListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, RestartGame);
    }
    public void Bresenham4Polygon(int X0, int Y0, int X1, int Y1, int j)
    {
        GameField gameField = gameObject.GetComponent<GameField>();

        int x0 = Y0;
        int y0 = X0;
        int x1 = Y1;
        int y1 = X1;
        //Изменения координат
        int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        int sx = (x1 >= x0) ? (1) : (-1);
        int sy = (y1 >= y0) ? (1) : (-1);

        if (dy < dx)
        {
            int d = (dy << 1) - dx;
            int d1 = dy << 1;
            int d2 = (dy - dx) << 1;

            //gameField.grid[x0, y0].setPixelState(true);
            ds[j].Add(d);

            int x = x0 + sx;
            int y = y0;
            for (int i = 1; i <= dx; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    y += sy;
                }
                else
                    d += d1;

                linePoints[j].Add(gameField.grid[x, y]);
                ds[j].Add(d);

                x += sx;
            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;

            //gameField.grid[x0, y0].setPixelState(true);
            ds[j].Add(d);

            int x = x0;
            int y = y0 + sy;
            for (int i = 1; i <= dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    x += sx;
                }
                else
                    d += d1;

                linePoints[j].Add(gameField.grid[x, y]);
                ds[j].Add(d);
                y += sy;
            }
        }
        last_point = linePoints[j][linePoints[j].Count - 1];
        //last_point.setPixelState(true);
    }
    public void gameCheck(GridPixelScript invoker)
    {
        if (!gameActive)
        {
            return;
        }
        if (!GetComponent<GameplayTimer>().Counting)
        {
            return;
        }
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
                GetComponent<GameplayTimer>().StopTimer();
                Messenger.Broadcast(GameEvents.GAME_OVER);
                return;
            }
            else
            {
                iteration = 0;
                GetComponent<GameField>().clearGrid();
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
            {
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            }
        }
    }
    public void GeneratePolygon()
    {
        GameField field = GetComponent<GameField>();
        Algorithms algorithms = GetComponent<Algorithms>();
        for(int i=0;i<linesQuantity;i++)
        {
            int firstX;
            int firstY;

            int secondX;
            int secondY;

            if(i==0)
            {
                 firstX = UnityEngine.Random.Range(0, 9);
                 firstY = UnityEngine.Random.Range(0, 9);

                 secondX = UnityEngine.Random.Range(0, 9);
                 secondY = UnityEngine.Random.Range(0, 9);

                while (Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > maxLineLength
                    || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < minLineLength)
                {
                    secondX = UnityEngine.Random.Range(0, 9);
                    secondY = UnityEngine.Random.Range(0, 9);
                }
            }
            else
            {
                firstX = lines[1, i - 1].Y;
                firstY = lines[1, i - 1].X;
                if (i == linesQuantity - 1)
                {
                    secondX = lines[0, 0].Y;
                    secondY = lines[0, 0].X;

                    while (true)
                    {
                        bool check = false;
                        for (int j = 0; j < i - 1; j++)
                        {
                            if (algorithms.segmentIntersection(lines[0, j], lines[1, j],
                                field.grid[firstY, firstX], field.grid[secondY, secondX]))
                            {
                                break;
                            }
                        }
                        if (check)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                        if (Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > maxLineLength
                        || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) <= minLineLength-1)
                        {
                            continue;
                        }
                        bool check = false;
                        for (int j = 0; j < i - 1; j++)
                        {
                            if (algorithms.segmentIntersection(lines[0, j], linePoints[j][linePoints[j].Count - 2],
                             field.grid[firstY, firstX], field.grid[secondY, secondX]))
                            {
                                break;
                            }
                        }
                        if (check)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }    
            }

            lines[0, i] = field.grid[firstY, firstX];
            lines[1, i] = field.grid[secondY, secondX];
            Bresenham4Polygon(firstX, firstY, secondX, secondY,i);
        }
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
            GameplayTimer timer = GetComponent<GameplayTimer>();
            switch (difficulty)
            {
                case 0:
                    timer.StartTime = 60f;
                    break;
                case 1:
                    timer.StartTime = 80f;
                    break;
                case 2:
                    timer.StartTime = 120f;
                    break;
                default:
                    timer.StartTime = 60f;
                    break;
            }
            timer.StartTimer();
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
        for (int i = 0; i < linesQuantity; i++)
        {
            ds[i].Clear();
            linePoints[i].Clear();
        }
        cur_line = 0;
        iteration = 0;
        switch (difficulty)
        {
            case 0:
                maxLengthSum = 20;
                break;
            case 1:
                maxLengthSum = 48;
                break;
            case 2:
                maxLengthSum = 90;
                break;
            default:
                maxLengthSum = 20;
                break;
        }
        GeneratePolygon();
        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        GetComponent<GameplayTimer>().timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
    public void SendStartGameEvent()
    {
        ProfilesManager pfManager = GetComponent<ProfilesManager>();
        if (PlayerPrefs.GetInt(pfManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit") == 1)
        {
            PlayerPrefs.SetInt(pfManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit", 0);
            PlayerPrefs.Save();
            Messenger.Broadcast(GameEvents.START_GAME);
        }
    }
}
