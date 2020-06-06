using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    private int _maxLengthSum;
    private int _minLineLength;
    private int _maxLineLength;
    private Pixel[,] _lines;
    private List<Pixel> _linePoints;
    private List<Pixel>[] _LinePoints;
    private List<int>[] _Ds;
    private List<int> _ds;
    private Pixel _last_point;
    private Pixel _prev_point;
    private int _iteration;
    private int _cur_line;
    private int _linesQuantity;
    private GameField _gameField;
    private InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(timer, difficulty)
    {
        textField = nextTextField;
        _gameField = inputField;
        switch (difficulty)
        {
            case 1:
                _linesQuantity = 7;
                _minLineLength = 4;
                _maxLineLength = 8;
                _maxLengthSum = 48;
                break;
            case 2:
                _linesQuantity = 10;
                _minLineLength = 5;
                _maxLineLength = 10;
                _maxLengthSum = 90;
                break;
            default:
                _linesQuantity = 5;
                _minLineLength = 2;
                _maxLineLength = 5;
                _maxLengthSum = 20;
                break;
        }

        _ds = new List<int>();
        _Ds = new List<int>[_linesQuantity];
        _LinePoints = new List<Pixel>[_linesQuantity];
        _linePoints = new List<Pixel>();
        _lines = new Pixel[2, _linesQuantity];

        GenerateLines();
        textField.text = _Ds[0][0].ToString();

        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);
        timer.Format = GameplayTimer.TimerFormat.smms;

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void CheckAction(Pixel invoker)
    {
        if (!CanCheckAction()) return;

        if (_prev_point == _last_point)
        {
            _cur_line++;
            if (_cur_line == _linesQuantity)
            {
                timer.StopTimer();
                Messenger.Broadcast(GameEvents.GAME_OVER);
                return;
            }
            else
            {
                _iteration = 0;
                _gameField.clearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                _lines[0, _cur_line].setPixelState(true);
                _last_point = _LinePoints[_cur_line][_LinePoints[_cur_line].Count - 1];
                _last_point.setPixelState(true);
                _prev_point = null;
                textField.text = _Ds[_cur_line][_iteration].ToString();
            }
        }
        else
        {
            _prev_point = _LinePoints[_cur_line][_iteration];

            if (invoker == _prev_point)
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

    public override void ChangeGameState()
    {
        if (!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
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
            gameActive = false;
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.clearGrid();
        _ds.Clear();
        for (var i = 0; i < _linesQuantity; i++)
        {
            _Ds[i].Clear();
            _linePoints.Clear();
        }
        _cur_line = 0;
        _iteration = 0;
        switch (difficulty)
        {
            case 1:
                _maxLengthSum = 48;
                break;
            case 2:
                _maxLengthSum = 90;
                break;
            default:
                _maxLengthSum = 20;
                break;
        }

        GenerateLines();

        timer.timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public void GenerateLines()
    {
        var maxLengthSum = _maxLengthSum;
        for (var i = 0; i < _linesQuantity; i++)
        {
            var firstX = UnityEngine.Random.Range(0, 9);
            var firstY = UnityEngine.Random.Range(0, 9);

            var secondX = UnityEngine.Random.Range(0, 9);
            var secondY = UnityEngine.Random.Range(0, 9);
            if (maxLengthSum > 0)
            {
                if (maxLengthSum - (int)Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < _minLineLength
                    && maxLengthSum - (int)Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) != 0)
                {
                    while (Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > _maxLineLength
               || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < _minLineLength)
                    {
                        firstX = UnityEngine.Random.Range(0, 9);
                        firstY = UnityEngine.Random.Range(0, 9);

                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                    }
                }

                else
                {
                    while (Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > _maxLineLength
               || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < _minLineLength)
                    {
                        firstX = UnityEngine.Random.Range(0, 9);
                        firstY = UnityEngine.Random.Range(0, 9);

                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                    }
                }
                maxLengthSum -= (int)Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY));
            }
            _lines[0, i] = _gameField.grid[firstY, firstX];
            _lines[1, i] = _gameField.grid[secondY, secondX];
            Algorithms.GetBrezenheimLineData(_gameField, firstX, firstY, secondX, secondY, out _ds, out _linePoints);
            _Ds[i] = new List<int>();
            _LinePoints[i] = new List<Pixel>();
            for (var j = 0; j < _ds.Count; j++)
            {
                _Ds[i].Add(_ds[j]);
            }
            for (var j = 0; j < _linePoints.Count; j++)
            {
                _LinePoints[i].Add(_linePoints[j]);
            }
            _ds.Clear();
            _linePoints.Clear();
        }
        _last_point = _LinePoints[0][_LinePoints[0].Count - 1];
        _prev_point = null;
        _lines[0, 0].setPixelState(true);
        _last_point.setPixelState(true);
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
