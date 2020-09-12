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
        var geometry = new Geometry();
        var maxLengthSum = _maxLengthSum;
        for (var i = 0; i < _linesQuantity; i++)
        {
            var x0 = UnityEngine.Random.Range(0, 9);
            var y0 = UnityEngine.Random.Range(0, 9);

            var x1 = UnityEngine.Random.Range(0, 9);
            var y1 = UnityEngine.Random.Range(0, 9);


            var line = new Line(new Position(x0, y0), new Position(x1, y1));
            var lineLength = line.GetLength();
            if (maxLengthSum > 0)
            {
                if (maxLengthSum - (int)lineLength < _minLineLength
                    && maxLengthSum - (int)lineLength != 0)
                {
                    while (lineLength> _maxLineLength
               || lineLength < _minLineLength)
                    {
                        x0 = UnityEngine.Random.Range(0, 9);
                        y0 = UnityEngine.Random.Range(0, 9);

                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);

                        var start = line.GetStart();
                        start.X = x0;
                        start.Y = y0;
                        var end = line.GetEnd();
                        end.X = x1;
                        end.Y = y1;
                        lineLength = line.GetLength();
                    }
                }

                else
                {
                    while (lineLength > _maxLineLength
               || lineLength < _minLineLength)
                    {
                        x0 = UnityEngine.Random.Range(0, 9);
                        y0 = UnityEngine.Random.Range(0, 9);

                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);

                        var start = line.GetStart();
                        start.X = x0;
                        start.Y = y0;
                        var end = line.GetEnd();
                        end.X = x1;
                        end.Y = y1;
                        lineLength = line.GetLength();
                    }
                }
                maxLengthSum -= (int)lineLength;
            }
            _lines[0, i] = _gameField.grid[y0, x0];
            _lines[1, i] = _gameField.grid[y1, x1];
            Algorithms.GetBrezenheimLineData(_gameField, x0, y0, x1, y1, out var ds, out var linePoints);
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

    ~BrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }
}
