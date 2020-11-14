public class GridPosition
{
    public int X { get; private set; }

    public int Y { get; private set; }

    public GridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(GridPosition other) => X == other.X && Y == other.Y;
}
