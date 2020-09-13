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

    public Position GetPoint(int index) => _linePoints[index];

    public int GetPointsCount() => _linePoints.Count;
}
