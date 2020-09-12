using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : GameMode
{
    private ILineGenerator _lineGenerator;
    private List<Line> _lines;
    private List<Position>[] _linePoints;
    private List<int>[] _ds;
    private Position _lastPoint;
    private Position _prevPoint;
    private int _curLine;
    private int _iteration;
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

        textField.text = _ds[0][0].ToString();

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

            if (_curLine == _lines.Count)
            {
                Messenger.Broadcast(GameEvents.TIMER_STOP);
                return;
            }

            if (_prevPoint == _lastPoint)
            {
                _curLine++;
                if (_curLine == _lines.Count)
                {
                    eventReactor.OnGameOver();
                    return;
                }
                else
                {
                    _iteration = 0;
                    _gameField.ClearGrid();
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                _gameField.grid[(int)_lines[_curLine].GetStart().X, (int)_lines[_curLine].GetStart().Y].setPixelState(true);
                _gameField.grid[(int)_lines[_curLine].GetEnd().X, (int)_lines[_curLine].GetEnd().Y].setPixelState(true);
                    _lastPoint = _linePoints[_curLine][_linePoints[_curLine].Count - 1];
                    _prevPoint = null;
                    textField.text = _ds[_curLine][_iteration].ToString();
                }
            }
            else
            {
                _prevPoint = _linePoints[_curLine][_iteration];

                if (invoker.X == _prevPoint.X && invoker.Y == _prevPoint.Y)
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    invoker.setPixelState(true);
                    _iteration++;
                    textField.text = _ds[_curLine][_iteration].ToString();
                    _prevPoint = _linePoints[_curLine][_iteration];
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

        _curLine = 0;
        _iteration = 0;

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

        _lines = new List<Line>(linesCount);
        _lineGenerator = new PolygonLineGenerator(minLength, maxLength);
        var lns = _lineGenerator.Generate(linesCount);
        _linePoints = new List<Position>[linesCount];
        _ds = new List<int>[linesCount];
        for (var i = 0; i < linesCount; i++)
        {
            _linePoints[i] = new List<Position>();
            _ds[i] = new List<int>();
        }

        for (var i = 0; i < linesCount; i++)
        {
            _lines[i] = lns[i];
            _linePoints[i] = Algorithms.GetBrezenheimLineData(_lines[i], out var dds);
            _ds[i] = dds;
        }

        _lastPoint = _linePoints[0][_linePoints[0].Count - 1];
        _gameField.grid[(int)_lines[0].GetStart().X, (int)_lines[0].GetStart().Y].setPixelState(true);
        _gameField.grid[(int)_lines[0].GetEnd().X, (int)_lines[0].GetEnd().Y].setPixelState(true);
    }
}
