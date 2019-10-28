using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
	[SerializeField] public GridPixelScript originalPixel;
    public GridPixelScript[,] grid;
    private int difficulty;
	private int gridRows = 10;
    private int gridCols = 10;
    private float offsetX;
    private float offsetY;

    public float OffsetX { get => offsetX; set => offsetX = value; }
    public float OffsetY { get => offsetY; set => offsetY = value; }
    public int GridRows { get => gridRows; set => gridRows = value; }
    public int GridCols { get => gridCols; set => gridCols = value; }
    public int Difficulty { get => difficulty; set => difficulty = value; }
    
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
        Vector3 startPos = originalPixel.transform.position;

        offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
		for (int i = 0; i < GridRows; i++)
        {
            for (int j = 0; j < GridCols; j++)
            {
                GridPixelScript pixel;
                
                if (i == 0 && j == 0)
                {
                    pixel = originalPixel;
                }
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clearGrid()
    {
        for(int i=0;i<GridRows;i++)
        {
            for(int j = 0;j<GridCols;j++)
            {
                grid[i, j].setPixelState(false);
            }
        }
    }
}