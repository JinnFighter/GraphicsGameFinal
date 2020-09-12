using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    private ILineGenerator _lineGenerator;
    private List<Line> _lines;
    private List<Position>[] _LinePoints;
    private List<int>[] _Ds;
    private Position _last_point;
    private Position _prev_point;
    private int _iteration;
    private int _cur_line;
    private int _linesCount;
    private GameField _gameField;
    private InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(difficulty)
    {
        textField = nextTextField;
        _gameField = inputField;
        
        _Ds = new List<int>[_linesCount];
        _LinePoints = new List<Position>[_linesCount];
        _lines = new List<Line>(_linesCount);

        GenerateLines();

        textField.text = _Ds[0][0].ToString();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void CheckAction(Pixel invoker)
    {
        if (!CanCheckAction()) return;

        if (_prev_point == _last_point)
        {
            _cur_line++;
            if (_cur_line == _linesCount)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
                eventReactor.OnGameOver();
                return;
            }
            else
            {
                _iteration = 0;
                _gameField.ClearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                _gameField.grid[(int)_lines[_cur_line].GetStart().X, (int)_lines[_cur_line].GetStart().Y].setPixelState(true);
                _last_point = _LinePoints[_cur_line][_LinePoints[_cur_line].Count - 1];
                _gameField.grid[(int)_last_point.X, (int)_last_point.Y].setPixelState(true);
              
                _prev_point = null;
                textField.text = _Ds[_cur_line][_iteration].ToString();
            }
        }
        else
        {
            _prev_point = _LinePoints[_cur_line][_iteration];

            if (invoker.X == _prev_point.X && invoker.Y == _prev_point.Y)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.setPixelState(true);
                _iteration++;
                textField.text = _Ds[_cur_line][_iteration].ToString();
                _prev_point = _LinePoints[_cur_line][_iteration];
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
        for (var i = 0; i < _linesCount; i++)
        {
            _Ds[i].Clear();
        }
        _cur_line = 0;
        _iteration = 0;
        GenerateLines();
        

        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    private void GenerateLines()
    {
        int minLength;
        int maxLength;
        int maxLengthSum;
        switch (difficulty)
        {
            case 1:
                _linesCount = 7;
                minLength = 4;
                maxLength = 8;
                maxLengthSum = 48;
                break;
            case 2:
                _linesCount = 10;
                minLength = 5;
                maxLength = 10;
                maxLengthSum = 90;
                break;
            default:
                _linesCount = 5;
                minLength = 2;
                maxLength = 5;
                maxLengthSum = 20;
                break;
        }
        _lineGenerator = new RandomLineGenerator(minLength, maxLength, maxLengthSum);
        _lines = _lineGenerator.Generate(_linesCount);

        foreach(var line in _lines)
        {
            var linePoints = Algorithms.GetBrezenheimLineData(line, out var ds);
            var i = _lines.IndexOf(line);
            _Ds[i] = new List<int>();
            _LinePoints[i] = new List<Position>();
            for (var j = 0; j < ds.Count; j++)
            {
                _Ds[i].Add(ds[j]);
            }
            for (var j = 0; j < linePoints.Count; j++)
            {
                _LinePoints[i].Add(linePoints[j]);
            }
        }
        _last_point = _LinePoints[0][_LinePoints[0].Count - 1];
        _prev_point = null;
        _gameField.grid[(int)_lines[0].GetStart().X, (int)_lines[0].GetStart().Y].setPixelState(true);
        _gameField.grid[(int)_last_point.X, (int)_last_point.Y].setPixelState(true);  
    }

    ~BrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }
}
