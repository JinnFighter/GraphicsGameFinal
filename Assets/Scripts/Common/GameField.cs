using UnityEngine;

public class GameField : MonoBehaviour
{
	[SerializeField] public Pixel originalPixel;
    public Pixel[,] grid;
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }
    public int Height { get; set; } = 10;
    public int Width { get; set; } = 10;
    public int Difficulty { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Difficulty = PlayerPrefs.GetInt("difficulty");
        switch(Difficulty)
        {
            case 0:
                Height = 10;
                Width = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
            case 1:
                Height = 15;
                Width = 15;
                originalPixel.transform.localScale = new Vector3(12, 12, 1);
                break;
            case 2:
                Height = 20;
                Width = 20;
                originalPixel.transform.localScale = new Vector3(10, 10, 1);
                break;
            default:
                Height = 10;
                Width = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
        }
        grid = new Pixel[Height, Width];
        var startPos = originalPixel.transform.position;

        OffsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        OffsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
		for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Pixel pixel;
                
                if (i == 0 && j == 0)
                    pixel = originalPixel;
                else
                {    
                    pixel = Instantiate(originalPixel) as Pixel;
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

    public void ClearGrid()
    {
        for(var i = 0; i < Height; i++)
        {
            for(var j = 0; j < Width; j++)
            {
                grid[i, j].setPixelState(false);
            }
        }
    }
}