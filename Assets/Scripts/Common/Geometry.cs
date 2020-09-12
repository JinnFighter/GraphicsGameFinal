using System;

public class Geometry
{
    public double GetLineLength(int x0, int y0, int x1, int y1) => 
        Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));

    public bool HasSegmentsIntersection(Pixel a, Pixel b, Pixel c, Pixel d)
    {
        int v1 = (d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X);
        int v2 = (d.X - c.X) * (b.Y - c.Y) - (d.Y - c.Y) * (b.X - c.X);
        int v3 = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        int v4 = (b.X - a.X) * (d.Y - a.Y) - (b.Y - a.Y) * (d.X - a.X);
        return (v1 * v2 < 0) && (v3 * v4 < 0);
    }
}
