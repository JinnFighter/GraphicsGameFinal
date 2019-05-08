using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class BrezenheimGameController : MonoBehaviour
{
    //[SerializeField] public GridPixelScript originalPixel;
    //private  GridPixelScript[,] grid;
    private int difficulty;
    private bool gameActive;
    private bool gameStarted;
    private int minLineLength;
    private int maxLineLength;
    private GridPixelScript[,] lines;
    private List<GridPixelScript> linePoints;
    private List<GridPixelScript>[] LinePoints;
    private List<int>[] Ds;
    private List<int> ds;
    private GridPixelScript last_point;
    private GridPixelScript prev_point;
    //public const int gridRows = 10;
    //public const int gridCols = 10;
    //private float offsetX;
    //private float offsetY;
    private int iteration;
    private int cur_line;
    private int linesQuantity;
    [SerializeField]private InputField textField;


    // Start is called before the first frame update
    void Start()
    {
        difficulty = GetComponent<GameField>().Difficulty;
        gameActive = false;
        gameStarted = false;
        switch(difficulty)
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
        //linesQuantity = 5;
        ds = new List<int>(1);
        Ds = new List<int>[linesQuantity];
        LinePoints = new List<GridPixelScript>[linesQuantity];
        linePoints = new List<GridPixelScript>(1);
        lines = new GridPixelScript[2, linesQuantity];
        GenerateLines();
        //Bresenham4Line(5, 4, 9, 9);
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.AddListener(GameEvents.RESTART_GAME, RestartGame);
        /*for (int i = 0; i < linesQuantity; i++)
        {
            for(int j=0;j<Ds[i].Count;j++)
            {
                Debug.Log(Ds[i][j]);
            }
        }*/
        GetComponent<GameplayTimer>().Format = GameplayTimer.TimerFormat.smms;
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
    public void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }
    public void drawLine(int X0, int Y0, int X1, int Y1)
    {
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

            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);

            //linePoints.Add(grid[x0, y0]);
            //ds.Add(d);
            GameField a = gameObject.GetComponent<GameField>();
            a.grid[x0, y0].setPixelState(true);
            //gameObject.GetComponent<GameField>().grid[x0, y0].setPixelState(true);
            //PutPixel(g, clr, x0, y0, 255);
            int x = x0 + sx;
            int y = y0;
            for (int i = 1; i <= dx; i++)
            {
                //Debug.Log(d.ToString());
                //ds.Add(d);
                if (d > 0)
                {
                    d += d2;
                    y += sy;

                }
                else
                    d += d1;

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);


                //linePoints.Add(grid[x, y]);
                //linePoints.Add(GetComponent<GameField>().grid[x, y]);
                //ds.Add(d);
                //GetComponent<GameField>().grid[x, y].setPixelState(true);
                
                a.grid[x, y].setPixelState(true);
                //PutPixel(g, clr, x, y, 255);
                x += sx;

            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;

            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);

            //linePoints.Add(grid[x0, y0]);
            //ds.Add(d);
            GameField a = gameObject.GetComponent<GameField>();
            a.grid[x0, y0].setPixelState(true);
            //gameObject.GetComponent<GameField>().grid[x0, y0].setPixelState(true);
            //PutPixel(g, clr, x0, y0, 255);
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

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);

                //linePoints.Add(grid[x, y]);
                //linePoints.Add(GetComponent<GameField>().grid[x, y]);
                //ds.Add(d);
                // GetComponent<GameField>().grid[x, y].setPixelState(true);
                
                a.grid[x0, y0].setPixelState(true);
                //PutPixel(g, clr, x, y, 255);
                y += sy;

            }
        }
    }
    public void Bresenham4Line( int X0, int Y0, int X1, int Y1)
        {
        GameField gameField = gameObject.GetComponent("GameField") as GameField;
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

            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);
            gameField.grid[x0, y0].setPixelState(true);
            //linePoints.Add(grid[x0, y0]);
            ds.Add(d);
            //PutPixel(g, clr, x0, y0, 255);
            int x = x0 + sx;
                int y = y0;
                for (int i = 1; i <= dx; i++)
                {
                //Debug.Log(d.ToString());
                //ds.Add(d);
                if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    
                    }
                    else
                        d += d1;

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);


                //linePoints.Add(grid[x, y]);
                //linePoints.Add(GetComponent<GameField>().grid[x,y]);
                linePoints.Add(gameField.grid[x,y]);
                ds.Add(d);
                //PutPixel(g, clr, x, y, 255);
                x +=sx;
                
            }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

            gameField.grid[x0, y0].setPixelState(true);
            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);

            //linePoints.Add(grid[x0, y0]);
            ds.Add(d);
            //PutPixel(g, clr, x0, y0, 255);
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

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);

                //linePoints.Add(grid[x, y]);
                linePoints.Add(GetComponent<GameField>().grid[x, y]);
                ds.Add(d);
                //PutPixel(g, clr, x, y, 255);
                y +=sy;
                
            }
            }
        //last_point = linePoints[linePoints.Count - 1];
        //linePoints[linePoints.Count - 1].setPixelState(true);
        }
    public void gameCheck(GridPixelScript invoker)
    {
        /*if(prev_point == last_point)
        {
            Debug.Log("Enough, start over, it's finished!");
            return;
        }
        else
        {
            prev_point = linePoints[iteration];
            
            if(invoker == prev_point)
            {
                Debug.Log("Correct!");
                invoker.setPixelState(true);//changle later to true! IMPORTANT!!!
                iteration++;
                textField.text = ds[iteration].ToString();
                prev_point = linePoints[iteration];
            }
            else
            {
                Debug.Log("Wrong!");
            }
            
        }*/
        if(!gameActive)
        {
            return;
        }
        if(!GetComponent<GameplayTimer>().Counting)
        {
            Debug.Log("Not Counting due to finish or no start");
            return;
        }
        if (cur_line == linesQuantity)
        {
            Debug.Log("Enough, start over, it's finished!");
            Messenger.Broadcast(GameEvents.TIMER_STOP);
            return;
        }
        if (prev_point==last_point)
        {
            cur_line++;
            if (cur_line==linesQuantity)
            {
                GetComponent<GameplayTimer>().StopTimer();
                Messenger.Broadcast(GameEvents.GAME_OVER);
                Debug.Log("Enough, start over, it's finished!");

                return;
            }
            else
            {
                iteration = 0;
                GetComponent<GameField>().clearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                lines[0, cur_line].setPixelState(true);
                last_point = LinePoints[cur_line][LinePoints[cur_line].Count - 1];
                last_point.setPixelState(true);
                prev_point = null;
                textField.text = Ds[cur_line][iteration].ToString();
            }
        }
        else
        {
            prev_point = LinePoints[cur_line][iteration];

            if (invoker == prev_point)
            {
                Debug.Log("Correct!");
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.setPixelState(true);//changle later to true! IMPORTANT!!!
                iteration++;
                textField.text = Ds[cur_line][iteration].ToString();
                prev_point = LinePoints[cur_line][iteration];
            }
            else
            {
                Debug.Log("Wrong!");
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            }
        }
    }
    public void GenerateLines()
    {
        for(int i=0;i< linesQuantity; i++)
        {
            int firstX = UnityEngine.Random.Range(0, 9);
            int firstY = UnityEngine.Random.Range(0, 9);

            int secondX = UnityEngine.Random.Range(0, 9);
            int secondY = UnityEngine.Random.Range(0, 9);
            while (Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > maxLineLength
                || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < minLineLength) 
            {
                firstX = UnityEngine.Random.Range(0, 9);
                firstY = UnityEngine.Random.Range(0, 9);

                secondX = UnityEngine.Random.Range(0, 9);
                secondY = UnityEngine.Random.Range(0, 9);
            }
            lines[0, i] = GetComponent<GameField>().grid[firstY,firstX];
            lines[1, i] = GetComponent<GameField>().grid[secondY, secondX];
            Bresenham4Line(firstX, firstY, secondX, secondY);
            Ds[i] = new List<int>();
            LinePoints[i] = new List<GridPixelScript>();
            for (int j = 0; j < ds.Count;j++)
            {
                Ds[i].Add(ds[j]);
            }
            for (int j = 0; j < linePoints.Count; j++)
            {
                LinePoints[i].Add(linePoints[j]);
            }
            //Ds[i] = ds;
            //LinePoints[i] = linePoints;
            ds.Clear();
            linePoints.Clear();
        }
        last_point = LinePoints[0][LinePoints[0].Count-1];
        GetComponent<GameField>().clearGrid();
        prev_point = null;
        lines[0, 0].setPixelState(true);
        //LinePoints[0][0].setPixelState(true);
        last_point.setPixelState(true);

    }
	public void Brezenheim4Circle(int Xc, int Yc, int r)
	{
		int xc = Yc;
		int yc = Xc;
		int x,y,d;
		x=0;
		y=r;
		d=3-2*y;
		while(x<=y)
		{
            //grid[x+xc,y+yc].setPixelState(true);
            //grid[x+xc,-y+yc].setPixelState(true);
            //grid[-x+xc,-y+yc].setPixelState(true);
            //grid[-x+xc,y+yc].setPixelState(true);
            //grid[y+xc,x+yc].setPixelState(true);
            //grid[y+xc,-x+yc].setPixelState(true);
            //grid[-y+xc,-x+yc].setPixelState(true);
            //grid[-y+xc,x+yc].setPixelState(true);

            GetComponent<GameField>().grid[x+xc,y+yc].setPixelState(true);
            GetComponent<GameField>().grid[x+xc,-y+yc].setPixelState(true);
            GetComponent<GameField>().grid[-x+xc,-y+yc].setPixelState(true);
            GetComponent<GameField>().grid[-x+xc,y+yc].setPixelState(true);
            GetComponent<GameField>().grid[y+xc,x+yc].setPixelState(true);
            GetComponent<GameField>().grid[y+xc,-x+yc].setPixelState(true);
            GetComponent<GameField>().grid[-y+xc,-x+yc].setPixelState(true);
            GetComponent<GameField>().grid[-y+xc,x+yc].setPixelState(true);

            if (d<0)
			{
				d=d+4*x+6;
			}
			else
			{
				d=d+4*(x-y)+10;
				y--;
			}
			x++;
		}
	}
    public void PauseGame()
    {
        gameActive = false;
        GetComponent<GameplayTimer>().PauseTimer();
    }
    public void ContinueGame()
    {
        gameActive = true;
        GetComponent<GameplayTimer>().ResumeTimer();
    }
    public void ChangeGameState()
    {
        if(!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
            switch(difficulty)
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
            //GetComponent<GameplayTimer>().StartTime = 60f;
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
        ds.Clear();
        for(int i = 0;i<linesQuantity;i++)
        {
            Ds[i].Clear();
            linePoints.Clear();
        }
        cur_line = 0;
        iteration = 0;
        GenerateLines();
        GetComponent<GameplayTimer>().timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
}
