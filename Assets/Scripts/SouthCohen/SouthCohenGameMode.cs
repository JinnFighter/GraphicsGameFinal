using System;
using System.Collections.Generic;
using UnityEngine;

public class SouthCohenGameMode : GameMode
{
    private Pixel[] _borderPoints;
    private List<Line> _lines;
    private int _iteration;
    private List<int>[] _lineZones;
    private SpriteRenderer _border;
    private int[,] _gridCodes;
    private int _gridCodesWidth;
    private int _gridCodesHeight;

    private GameField _gameField;

    public SouthCohenGameMode(GameplayTimer timer, SpriteRenderer border, GameField field, int difficulty) : base(difficulty)
    {
        _gameField = field;
        difficulty = _gameField.Difficulty;
        _border = border;

        GenerateBorder(difficulty);

        DoRestartAction();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    ~SouthCohenGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }

    public void SouthCohen(Line line, Position left, Position right, int i)
    {
        var start = line.GetStart();
        var end = line.GetEnd();
        var ax = start.X;
        var ay = start.Y;
        var bx = end.X;
        var by = end.Y;
        var code1 = Code(start, left, right);
        var code2 = Code(end, left, right);
        var inside = (code1 | code2) == 0;
        var outside = (code1 & code2) != 0;
        while (!inside && !outside)
        {
            if (code1 == 0)
            {
                Swap(ref ax, ref bx);
                Swap(ref ay, ref by);
                int c = code1;
                code1 = code2;
                code2 = c;
            }

            if (Convert.ToBoolean(code1 & 0x01))
            {
                ay += (left.X - ax) * (by - ay) / (bx - ax);
                ax = left.X;
                if (!_lineZones[i].Contains(code1))
                    _lineZones[i].Add(code1);
            }

            if (Convert.ToBoolean(code1 & 0x02))
            {
                ax += (left.Y - ay) * (bx - ax) / (by - ay);
                ay = left.Y;
                if (!_lineZones[i].Contains(code1))
                    _lineZones[i].Add(code1);
            }

            if (Convert.ToBoolean(code1 & 0x04))
            {
                ay += (right.X - ax) * (by - ay) / (bx - ax);
                ax = right.X;
                if (!_lineZones[i].Contains(code1))
                    _lineZones[i].Add(code1);
            }

            if (Convert.ToBoolean(code1 & 0x08))
            {
                ax += (right.Y - ay) * (bx - ax) / (by - ay);
                ay = right.Y;
                if (!_lineZones[i].Contains(code1))
                    _lineZones[i].Add(code1);
            }

            code1 = Code(new Position(ax, ay), left, right);
            inside = (code1 | code2) == 0;
            outside = (code1 & code2) != 0;
        }
    }

    private int Code(Position point, Position topLeft, Position downRight)
    {
        var code = 0;
        if (point.X < topLeft.X) code |= 0x01;//_ _ _ 1;
        if (point.X > downRight.X) code |= 0x04;//_ 1 _ _;
        if (point.Y < topLeft.Y) code |= 0x02;//_ _ 1 _;
        if (point.Y > downRight.Y) code |= 0x08;//1 _ _ _;
        return code;
    }

    public void GenerateLines()
    {
        int b;
        int minLength;
        int maxLength;
        int linesCount;
        switch (difficulty)
        {
            case 1:
                linesCount = 7;
                maxLength = 10;
                minLength = 8;
                b = 14;
                break;
            case 2:
                linesCount = 10;
                maxLength = 11;
                minLength = 10;
                b = 19;
                break;
            default:
                linesCount = 5;
                maxLength = 8;
                minLength = 5;
                b = 9;
                break;
        }

        _lineZones = new List<int>[linesCount];
        for (var i = 0; i < linesCount; i++)
            _lineZones[i] = new List<int>();

        _lines = new List<Line>(linesCount);

        for (var i = 0; i < linesCount; i++)
        {
            var firstX = UnityEngine.Random.Range(0, b);
            var firstY = UnityEngine.Random.Range(0, b);

            var secondX = UnityEngine.Random.Range(0, b);
            var secondY = UnityEngine.Random.Range(0, b);

            while ((Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > maxLength
              || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < minLength)
              || (!CheckIntersection(firstX, firstY, secondX, secondY)))
            {
                firstX = UnityEngine.Random.Range(0, b);
                firstY = UnityEngine.Random.Range(0, b);

                secondX = UnityEngine.Random.Range(0, b);
                secondY = UnityEngine.Random.Range(0, b);
            }
            _lines[i] = new Line(new Position(firstY, firstX), new Position(secondY, secondX));
        }
    }

    public bool CheckIntersection(int Ax, int Ay, int Bx, int By)
    {
        var ax = Ax;
        var ay = Ay;
        var bx = Bx;
        var by = By;
        if (ax > bx)
        {
            swap(ax, bx);
            swap(ay, by);
        }
        int[,] matr = new int[2, 2];

        matr[0, 0] = ax.CompareTo(_borderPoints[0].X) + ax.CompareTo(_borderPoints[1].X);
        matr[0, 1] = ay.CompareTo(_borderPoints[0].Y) + ay.CompareTo(_borderPoints[1].Y);
        matr[1, 0] = bx.CompareTo(_borderPoints[0].X) + bx.CompareTo(_borderPoints[1].X);
        matr[1, 1] = by.CompareTo(_borderPoints[0].Y) + by.CompareTo(_borderPoints[1].Y);
        int checker = matr[0, 0];
        if ((checker == matr[0, 1]) && (checker == matr[1, 0]) && (checker == matr[1, 1]))
            return false;
        else
        {
            var res = (matr[0, 0] * matr[1, 1]) - (matr[1, 0] * matr[0, 1]);
            if (res == 0)
                return true;
            else
                return false;
        }
    }

    public override void Check(Pixel invoker)
    {
        if (_iteration == _lines.Count)
        {
            Messenger.Broadcast(GameEvents.GAME_OVER);
            return;
        }
        var check = false;
        var c = -1;

        foreach (var a in _lineZones[_iteration])
        {
            if (a == this.Code(invoker.GetPosition(), _borderPoints[0].GetPosition(), _borderPoints[1].GetPosition()))
            {
                check = true;
                c = a;
                break;
            }
        }
        if (check)
        {
            ClearZone(c);
            _lineZones[_iteration].Remove(c);
            Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);

            if (_lineZones[_iteration].Count == 0)
                _iteration++;
            else
                return;

            if (_iteration == _lines.Count)
            {
                eventReactor.OnGameOver();
            }
            else
            {
                _gameField.ClearGrid();

                var linePts = Algorithms.GetBrezenheimLineData(_lines[_iteration], out _);
                _gameField.Draw(linePts);
            }
        }
        else
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
    }

    public void ClearZone(int code)
    {
        for (var i = 0; i < _gridCodesHeight; i++)
        {
            for (var j = 0; j < _gridCodesWidth; j++)
            {
                if (_gridCodes[i, j] == code)
                {
                    if (!_gameField.grid[i, j].pixel_empty.activeSelf)
                        _gameField.grid[i, j].setPixelState(false);
                }
            }
        }
    }

    public override void DoRestartAction()
    {
        _gameField.ClearGrid();
        for (var i = 0; i < _lines.Count; i++)
            _lineZones[i].Clear();

        _iteration = 0;
        GenerateLines();

        for (var i = 0; i < _lines.Count; i++)
            SouthCohen(_lines[i], _borderPoints[0].GetPosition(), _borderPoints[1].GetPosition(), i);
        _iteration = 0;

        var linePts = Algorithms.GetBrezenheimLineData(_lines[0], out _);
        _gameField.Draw(linePts);
    }

    private void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    private void swap<T>(T a, T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    private void GenerateBorder(int difficulty)
    {
        _borderPoints = new Pixel[2];
        Vector3 pos;
        Vector3 scale;
        switch (difficulty)
        {
            case 1:
                _borderPoints[0] = _gameField.grid[2, 2];
                _borderPoints[1] = _gameField.grid[8, 8];
                pos = _borderPoints[0].transform.position;
                pos.x += 9.5f;
                pos.y -= 9.5f;
                pos.z = _border.transform.position.z;
                _border.transform.position = pos;
                scale = _border.transform.localScale;
                scale.x *= 7.5f;
                scale.y *= 7.5f;
                break;
            case 2:
                _borderPoints[0] = _gameField.grid[2, 2];
                _borderPoints[1] = _gameField.grid[11, 11];
                pos = _borderPoints[0].transform.position;
                pos.x += 14.5f;
                pos.y -= 14.5f;
                pos.z = _border.transform.position.z;
                _border.transform.position = pos;
                scale = _border.transform.localScale;
                scale.x *= 10;
                scale.y *= 10;
                break;
            default:
                _borderPoints[0] = _gameField.grid[3, 3];
                _borderPoints[1] = _gameField.grid[7, 7];
                pos = _borderPoints[0].transform.position;
                pos.x += 12.5f;
                pos.y -= 12.5f;
                pos.z = _border.transform.position.z;
                _border.transform.position = pos;
                scale = _border.transform.localScale;
                scale.x *= 10;
                scale.y *= 10;
                break;
        }
        _border.transform.localScale = scale;

        _gridCodesWidth = _gameField.Width;
        _gridCodesHeight = _gameField.Height;
        _gridCodes = new int[_gridCodesHeight, _gridCodesWidth];
        for (var i = 0; i < _gridCodesHeight; i++)
            for (var j = 0; j < _gridCodesWidth; j++)
                _gridCodes[i, j] = this.Code(new Position(i, j), _borderPoints[0].GetPosition(), _borderPoints[1].GetPosition());
    }
}
