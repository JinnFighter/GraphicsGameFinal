public class NewGameField
{
    private CellData[,] _grid;
    private int _width;
    private int _height;

    public NewGameField(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                _height = 15;
                _width = 15;
                break;
            case 2:
                _height = 20;
                _width = 20;
                break;
            default:
                _height = 10;
                _width = 10;
                break;
        }
        _grid = new CellData[_width, _height];
        for (var i = 0; i < _width; i++)
            for (var j = 0; j < _height; j++)
                _grid[i, j] = new CellData(new Position(i, j), false);
    }

    public CellData GetData(int x, int y) => _grid[x, y];

    public int GetWidth() => _width;

    public int GetHeight() => _height;
}
