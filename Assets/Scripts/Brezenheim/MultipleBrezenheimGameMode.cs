using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : GameMode
{
    private ILineGenerator _lineGenerator;
    private Line[] lines;
    private List<Position>[] linePoints;
    private List<int>[] ds;
    private Position last_point;
    private Position prev_point;
    private int cur_line;
    private int iteration;
    private InputField textField;
    private GameField _gameField;

    public MultipleBrezenheimGameMode(GameplayTimer timer, int diff, GameField inputField, InputField nextTextField) : base(diff)
    {
        textField = nextTextField;
        _gameField = inputField;
        difficulty = diff;
        gameActive = false;
        gameStarted = false;

        Generate();

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

            if (cur_line == lines.Length)
            {
                Messenger.Broadcast(GameEvents.TIMER_STOP);
                return;
            }

            if (prev_point == last_point)
            {
                cur_line++;
                if (cur_line == lines.Length)
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

        cur_line = 0;
        iteration = 0;

        Generate();

        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    private void Generate()
    {
        int linesCount;
        int minLength;
        int maxLength;
        switch (difficulty)
        {
            case 1:
                linesCount = 7;
                minLength = 4;
                maxLength = 8;
                break;
            case 2:
                linesCount = 10;
                minLength = 5;
                maxLength = 10;
                break;
            default:
                linesCount = 5;
                minLength = 2;
                maxLength = 5;
                break;
        }

        lines = new Line[linesCount];
        _lineGenerator = new PolygonLineGenerator(minLength, maxLength);
        var lns = _lineGenerator.Generate(linesCount);
        linePoints = new List<Position>[linesCount];
        ds = new List<int>[linesCount];
        for (var i = 0; i < linesCount; i++)
        {
            linePoints[i] = new List<Position>();
            ds[i] = new List<int>();
        }

        for (var i = 0; i < linesCount; i++)
        {
            lines[i] = lns[i];
            linePoints[i] = Algorithms.GetBrezenheimLineData(lines[i], out var dds);
            ds[i] = dds;
        }

        last_point = linePoints[0][linePoints[0].Count - 1];
        _gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].setPixelState(true);
        _gameField.grid[(int)lines[0].GetEnd().X, (int)lines[0].GetEnd().Y].setPixelState(true);
    }
}
