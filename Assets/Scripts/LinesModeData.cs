using System.Collections.Generic;

public class LinesModeData
{
    private List<Position> _linePoints;
    private int _currentPoint;

    public LinesModeData()
    {
        _linePoints = new List<Position>();
        _currentPoint = 0;
    }

    public Position GetCurrentPoint() => _linePoints[_currentPoint];

    public void AddPoint(Position point) => _linePoints.Add(point);

    public Position GetPoint(int index) => _linePoints[index];

    public int GetPointsCount() => _linePoints.Count;

    public bool IsCurrentLast() => _currentPoint == _linePoints.Count;

    public void NextPoint() => _currentPoint++;

    public void ClearCounter() => _currentPoint = 0;

    public void Clear()
    {
        _linePoints.Clear();
        _currentPoint = 0;
    }

    public int GetCurrentIndex() => _currentPoint;
}
