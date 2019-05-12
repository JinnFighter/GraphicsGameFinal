using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SouthCohenController : MonoBehaviour
{
    private bool gameActive;
    private bool gameStarted;
    private int difficulty;
    private GridPixelScript[] borderPoints;
    private int linesQuantity;
    private GridPixelScript[,] lines;
    private int iteration;
    private List<int>[] lineZones;
    private int borderWidth;
    private int borderHeight;
    [SerializeField] private SpriteRenderer border;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = GetComponent<GameField>().Difficulty;
        gameActive = false;
        gameStarted = false;
        linesQuantity =1;
        lineZones = new List<int>[linesQuantity];
        for(int i = 0;i<linesQuantity;i++)
        {
            lineZones[i] = new List<int>();
        }
        lines = new GridPixelScript[2, linesQuantity];
        borderWidth = 5;
        borderHeight = 5;
        borderPoints = new GridPixelScript[2];
        GameField gameField = gameObject.GetComponent<GameField>();
        lines[0, 0] = gameField.grid[0, 1];
        lines[1, 0] = gameField.grid[9, 9];
        borderPoints[0] = gameField.grid[3, 3];
        borderPoints[1] = gameField.grid[7, 7];
        Vector3 pos = borderPoints[0].transform.position;
        pos.x += 12.5f;
        pos.y -= 12.5f;
        pos.z = border.transform.position.z;
        border.transform.position = pos;
        Vector3 scale = border.transform.localScale;
        scale.x = (scale.x) * 10;
        scale.y = (scale.y ) * 10;
        border.transform.localScale = scale;

        //border.transform.localScale.Set(border.transform.localScale.x + borderWidth, border.transform.localScale.y + borderHeight, border.transform.localScale.z);
        //border.sprite.rect.size.Set(border.sprite.rect.size.x*borderWidth, border.sprite.rect.size.y*borderHeight);
        //gameObject.GetComponent<Algorithms>().southCohen(gameField.grid[0, 0], gameField.grid[9, 9], 
        southCohen(lines[0,0], lines[1,0],
            borderPoints[0], borderPoints[1]);
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.AddListener(GameEvents.RESTART_GAME, RestartGame);
        
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
    public void gameCheck(GridPixelScript invoker)
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
        
        if(iteration==linesQuantity)
        {
            Messenger.Broadcast(GameEvents.GAME_OVER);
            return;
        }
        bool check = false;
        int c=-1;
        foreach (int a in lineZones[iteration])
        {
            if(a==Code(invoker, borderPoints[0], borderPoints[1]))
            {
                
                check = true;
                Debug.Log(a);
                c = a;
                //lineZones[iteration].Remove(a);
                break;
            }
        }
        if(check)
        {
            Debug.Log("Correct");
            lineZones[iteration].Remove(c);
            Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
           
            if (lineZones[iteration].Count == 0)
            {
                iteration++;
            }
            if (iteration == linesQuantity)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
            }
        }
        else
        {
            Debug.Log("Wrong");
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
       // GetComponent<Algorithms>().Code(invoker, borderPoints[0], borderPoints[1]);
        //invoker.setPixelState(true);
    }
    public void GenerateLines()
    {
        for(int i=0;i<linesQuantity;i++)
        {
            int firstX = UnityEngine.Random.Range(0, 9);
            int firstY = UnityEngine.Random.Range(0, 9);

            int secondX = UnityEngine.Random.Range(0, 9);
            int secondY = UnityEngine.Random.Range(0, 9);
            lines[0, i] = GetComponent<GameField>().grid[firstY, firstX];
            lines[1, i] = GetComponent<GameField>().grid[secondY, secondX];
        }
    }
    public void southCohen(GridPixelScript nA, GridPixelScript nB, GridPixelScript rectLeft, GridPixelScript rectRight)
    {
        //Point A = new Point();
        //A = nA;
        //Point B = new Point();
        //B = nB;
       
        GridPixelScript A = nA;
        GridPixelScript B = nB;
        int ax = A.X;
        int ay = A.Y;
        int bx = B.X;
        int by = B.Y;
       int code1 = this.Code(A, rectLeft, rectRight);
        int code2 = this.Code(B, rectLeft, rectRight);
        bool inside = (code1 | code2) == 0;
        bool outside = (code1 & code2) != 0;
        while (!inside && !outside)
        {
            if (code1 == 0)
            {
                //Swap;
                //GetComponent<Algorithms>().Swap(ref A, ref B);
                GetComponent<Algorithms>().Swap(ref ax,ref bx);
                GetComponent<Algorithms>().Swap(ref ay,ref by);
                int c = code1;
                code1 = code2;
                code2 = c;
            }
            if (Convert.ToBoolean(code1 & 0x01))
            {
                //A.Y += (rectLeft.X - A.X) * (B.Y - A.Y) / (B.X - A.X);
                //A.X = rectLeft.X;
                ay += (rectLeft.X - ax) * (by - ay) / (bx - ax);
                ax = rectLeft.X;
                if(!lineZones[0].Contains(code1))
                    lineZones[0].Add(code1);
            }
            if (Convert.ToBoolean(code1 & 0x02))
            {
                //A.X += (rectLeft.Y - A.Y) * (B.X - A.X) / (B.Y - A.Y);
                //A.Y = rectLeft.Y;
                ax += (rectLeft.Y - ay) * (bx - ax) / (by - ay);
                ay = rectLeft.Y;
                if (!lineZones[0].Contains(code1))
                    lineZones[0].Add(code1);
            }
            if (Convert.ToBoolean(code1 & 0x04))
            {
                //A.Y += (rectRight.X - A.X) * (B.Y - A.Y) / (B.X - A.X);
                //A.X = rectRight.X;
                ay += (rectRight.X - ax) * (by - ay) / (bx - ax);
                ax = rectRight.X;
                if (!lineZones[0].Contains(code1))
                    lineZones[0].Add(code1);
            }
            if (Convert.ToBoolean(code1 & 0x08))
            {
                //A.X += (rectRight.Y - A.Y) * (B.X - A.X) / (B.Y - A.Y);
                //A.Y = rectRight.Y;
                ax += (rectRight.Y - ay) * (bx - ax) / (by - ay);
                ay = rectRight.Y;
                if (!lineZones[0].Contains(code1))
                    lineZones[0].Add(code1);
            }

            //code1 = Code(A, rectLeft, rectRight);
            code1 = Code(GetComponent<GameField>().grid[ax,ay], rectLeft, rectRight);
            inside = (code1 | code2) == 0;
            outside = (code1 & code2) != 0;

        }
        if (!outside)
            //drawLine(A, B, Color.Blue);
            //GetComponent<Algorithms>().drawLine(A.X, A.Y, B.X, B.Y);
            GetComponent<Algorithms>().drawLine(ax, ay, bx, by);

    }
    public int Code(GridPixelScript point, GridPixelScript rectLeft, GridPixelScript rectRight)
    {
        int code = 0;
        if (point.X < rectLeft.X) code |= 0x01;//_ _ _ 1;
        if (point.X > rectRight.X) code |= 0x04;//_ 1 _ _;
        if (point.Y < rectLeft.Y) code |= 0x02;//_ _ 1 _;
        if (point.Y > rectRight.Y) code |= 0x08;//1 _ _ _;
        return code;
    }
    public void ClearZone(int code)
    {
        if(Convert.ToBoolean(code & 0x01))
        {

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
        //ds.Clear();
        for (int i = 0; i < linesQuantity; i++)
        {
            //Ds[i].Clear();
            //linePoints.Clear();
        }
        //cur_line = 0;
        iteration = 0;
        switch (difficulty)
        {
            case 0:
                //maxLengthSum = 20;
                break;
            case 1:
                //maxLengthSum = 48;
                break;
            case 2:
                //maxLengthSum = 90;
                break;
            default:
                //maxLengthSum = 20;
                break;
        }
        GenerateLines();

        GetComponent<GameplayTimer>().timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
}
