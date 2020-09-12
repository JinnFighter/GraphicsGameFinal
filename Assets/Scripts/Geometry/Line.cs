using System;

public class Line
{
    private Position _start;
    private Position _end;

    public Line(Position start, Position end)
    {
        _start = start;
        _end = end;
    }

    public double GetLength() 
    {
        var xdif = _end.X - _start.X;
        var ydif = _end.Y - _start.Y;
        return Math.Sqrt(xdif * xdif + ydif * ydif); 
    }
}
