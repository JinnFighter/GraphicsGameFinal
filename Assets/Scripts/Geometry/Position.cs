public class Position
{
    public double X { get; set; }

    public double Y { get; set; }

    public Position(double x, double y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Position other) => X == other.X && Y == other.Y;
}
