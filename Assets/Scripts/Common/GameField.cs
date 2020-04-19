using UnityEngine;

public class GameField : MonoBehaviour
{
	[SerializeField] public GridPixelScript originalPixel;
    public GridPixelScript[,] grid;
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }
    public int GridRows { get; set; } = 10;
    public int GridCols { get; set; } = 10;
    public int Difficulty { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Difficulty = PlayerPrefs.GetInt("difficulty");
        switch(Difficulty)
        {
            case 0:
                GridRows = 10;
                GridCols = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
            case 1:
                GridRows = 15;
                GridCols = 15;
                originalPixel.transform.localScale = new Vector3(12, 12, 1);
                break;
            case 2:
                GridRows = 20;
                GridCols = 20;
                originalPixel.transform.localScale = new Vector3(10, 10, 1);
                break;
            default:
                GridRows = 10;
                GridCols = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
        }
        grid = new GridPixelScript[GridRows, GridCols];
        var startPos = originalPixel.transform.position;

        OffsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        OffsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
		for (var i = 0; i < GridRows; i++)
        {
            for (var j = 0; j < GridCols; j++)
            {
                GridPixelScript pixel;
                
                if (i == 0 && j == 0)
                    pixel = originalPixel;
                else
                {    
                    pixel = Instantiate(originalPixel) as GridPixelScript;
                    float posX = (OffsetX * j) + startPos.x;
                    float posY = -(OffsetY * i) + startPos.y;
                    pixel.transform.position = new Vector3(posX, posY, startPos.z);
                }

                pixel.X = i;
                pixel.Y = j;
                grid[i,j] = pixel;  
            }
        }
    }

    public void clearGrid()
    {
        for(var i = 0; i < GridRows; i++)
        {
            for(var j = 0; j < GridCols; j++)
            {
                grid[i, j].setPixelState(false);
            }
        }
    }
}