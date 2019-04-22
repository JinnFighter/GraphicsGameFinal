using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
	[SerializeField] public GridPixelScript originalPixel;
    //[SerializeField] public string fieldSize;
   [SerializeField] private string fieldSize;
    public GridPixelScript[,] grid;
	private int gridRows = 10;
    private int gridCols = 10;
    private float offsetX;
    private float offsetY;

    public float OffsetX { get => offsetX; set => offsetX = value; }
    public float OffsetY { get => offsetY; set => offsetY = value; }
    public int GridRows { get => gridRows; set => gridRows = value; }
    public int GridCols { get => gridCols; set => gridCols = value; }



    // Start is called before the first frame update
    void Start()
    {
        //fieldSize = "small";
	switch(fieldSize)
	{
		case "small":
			GridRows = 10;
			GridCols = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
		case "medium":
			GridRows = 30;
			GridCols = 50;
                originalPixel.transform.localScale =new Vector3 (12,12,1);
		break;
		case "big":
			GridRows = 50;
			GridCols = 80;
                originalPixel.transform.localScale = new Vector3(10, 10, 1);
                break;
		case "enormous":
			GridRows = 80;
			GridCols = 100;
                
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
