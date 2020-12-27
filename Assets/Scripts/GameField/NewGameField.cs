using UnityEngine;

public class NewGameField : MonoBehaviour
{
    public int Difficulty { get; private set; }

    private CellData[,] _grid;

    void Awake()
    {
        int width;
        int height;
        Difficulty = PlayerPrefs.GetInt("difficulty");
        switch (Difficulty)
        {
            case 1:
                height = 15;
                width = 15;
                break;
            case 2:
                height = 20;
                width = 20;
                break;
            default:
                height = 10;
                width = 10;
                break;
        }
        _grid = new CellData[height, width];
        for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
                _grid[i, j] = new CellData(new Position(i, j), false);
    }
}
