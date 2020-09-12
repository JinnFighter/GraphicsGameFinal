using System;

public class Geometry
{
    public double GetLineLength(int x0, int y0, int x1, int y1) => 
        Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
}
