using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : GameMode
{
    private Line[] lines;
    private int linesQuantity;
    private List<Position>[] linePoints;
    private List<int>[] ds;
    private Position last_point;
    private Position prev_point;
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
        lines = new Line[linesQuantity];
        linePoints = new List<Position>[linesQuantity];
        ds = new List<int>[linesQuantity];
        for (var i = 0; i < linesQuantity; i++)
        {
            linePoints[i] = new List<Position>();
            ds[i] = new List<int>();
        }

        GeneratePolygon();

        last_point = linePoints[0][linePoints[0].Count - 1];
        _gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].setPixelState(true);
        _gameField.grid[(int)lines[0].GetEnd().X, (int)lines[0].GetEnd().Y].setPixelState(true);
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
                _gameField.grid[(int)lines[cur_line].GetStart().X, (int)lines[cur_line].GetStart().Y].setPixelState(true);
                _gameField.grid[(int)lines[cur_line].GetEnd().X, (int)lines[cur_line].GetEnd().Y].setPixelState(true);
                    last_point = linePoints[cur_line][linePoints[cur_line].Count - 1];
                    prev_point = null;
                    textField.text = ds[cur_line][iteration].ToString();
                }
            }
            else
            {
                prev_point = linePoints[cur_line][iteration];

                if (invoker.X == prev_point.X && invoker.Y == prev_point.Y)
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
        _gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].setPixelState(true);
        _gameField.grid[(int)lines[0].GetEnd().X, (int)lines[0].GetEnd().Y].setPixelState(true);
        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public void GeneratePolygon()
    {
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

                var line = new Line(new Position(x0, y0), new Position(x1, y1));
                var lineLength = line.GetLength();
                while (lineLength > maxLineLength
                    || lineLength < minLineLength)
                {
                    x1 = UnityEngine.Random.Range(0, 9);
                    y1 = UnityEngine.Random.Range(0, 9);
                    var end = line.GetEnd();
                    end.X = x1;
                    end.Y = y1;
                    lineLength = line.GetLength();
                }
            }
            else
            {
                x0 = (int)lines[i - 1].GetEnd().Y;
                y0 = (int)lines[i - 1].GetEnd().X;
                if (i == linesQuantity - 1)
                {
                    x1 = (int)lines[0].GetStart().Y;
                    y1 = (int)lines[0].GetStart().X;

                    while (true)
                    {
                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (HasSegmentsIntersection(lines[j].GetStart(), lines[j].GetEnd(),
                                new Position(y0, x0), new Position(y1, x1)))
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
                        var line = new Line(new Position(x0, y0), new Position(x1, y1));
                        var lineLength = line.GetLength();
                        if (lineLength > maxLineLength
                        || lineLength <= minLineLength - 1)
                        {
                            var end = line.GetEnd();
                            end.X = x1;
                            end.Y = y1;
                            lineLength = line.GetLength();
                            continue;
                        }

                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (HasSegmentsIntersection(lines[j].GetStart(), linePoints[j][linePoints[j].Count - 2],
                             new Position(y0, x0), new Position(y1, x1)));
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

            lines[i] = new Line(new Position(y0, x0), new Position(y1, x1));
            linePoints[i] = Algorithms.GetBrezenheimLineData(new Line(new Position(x0, y0), new Position(x1, y1)), out ds[i]);
        }
    }

    private bool HasSegmentsIntersection(Position a, Position b, Position c, Position d)
    {
        int v1 = (int)((d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X));
        int v2 = (int)((d.X - c.X) * (b.Y - c.Y) - (d.Y - c.Y) * (b.X - c.X));
        int v3 = (int)((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X));
        int v4 = (int)((b.X - a.X) * (d.Y - a.Y) - (b.Y - a.Y) * (d.X - a.X));
        return (v1 * v2 < 0) && (v3 * v4 < 0);
    }
}
