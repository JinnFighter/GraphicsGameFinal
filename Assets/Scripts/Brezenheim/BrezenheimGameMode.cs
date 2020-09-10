using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    private int _maxLengthSum;
    private int _minLineLength;
    private int _maxLineLength;
    private Pixel[,] _lines;
    private List<Pixel>[] _LinePoints;
    private List<int>[] _Ds;
    private Pixel _last_point;
    private Pixel _prev_point;
    private int _iteration;
    private int _cur_line;
    private int _linesQuantity;
    private GameField _gameField;
    private InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(difficulty)
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
        _Ds = new List<int>[_linesQuantity];
        _LinePoints = new List<Pixel>[_linesQuantity];
        _lines = new Pixel[2, _linesQuantity];

        GenerateLines();
        textField.text = _Ds[0][0].ToString();

        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);

        eventReactor = new DefaultReactor(timer, difficulty);

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
                Messenger.Broadcast(GameEvents.GAME_OVER);
                eventReactor.OnGameOver();
                return;
            }
            else
            {
                _iteration = 0;
                _gameField.ClearGrid();
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
            eventReactor.OnChangeState(difficulty);
        }
        else
            gameActive = false;
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.ClearGrid();
        for (var i = 0; i < _linesQuantity; i++)
        {
            _Ds[i].Clear();
        }
        _cur_line = 0;
        _iteration = 0;
        switch(difficulty)
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

        eventReactor.OnRestart();
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
                if (maxLengthSum - (int)GetLineLength(firstX, firstY, secondX, secondY) < _minLineLength
                    && maxLengthSum - (int)GetLineLength(firstX, firstY, secondX, secondY) != 0)
                {
                    while (GetLineLength(firstX, firstY, secondX, secondY) > _maxLineLength
               || GetLineLength(firstX, firstY, secondX, secondY) < _minLineLength)
                    {
                        firstX = UnityEngine.Random.Range(0, 9);
                        firstY = UnityEngine.Random.Range(0, 9);

                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                    }
                }

                else
                {
                    while (GetLineLength(firstX, firstY, secondX, secondY) > _maxLineLength
               || GetLineLength(firstX, firstY, secondX, secondY) < _minLineLength)
                    {
                        firstX = UnityEngine.Random.Range(0, 9);
                        firstY = UnityEngine.Random.Range(0, 9);

                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                    }
                }
                maxLengthSum -= (int)GetLineLength(firstX, firstY, secondX, secondY);
            }
            _lines[0, i] = _gameField.grid[firstY, firstX];
            _lines[1, i] = _gameField.grid[secondY, secondX];
            Algorithms.GetBrezenheimLineData(_gameField, firstX, firstY, secondX, secondY, out var ds, out var linePoints);
            _Ds[i] = new List<int>();
            _LinePoints[i] = new List<Pixel>();
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
        _lines[0, 0].setPixelState(true);
        _last_point.setPixelState(true);
    }

    private double GetLineLength(int x0, int y0, int x1, int y1) => Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));

    ~BrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }
}
