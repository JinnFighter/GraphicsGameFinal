using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
	[SerializeField] public GridPixelScript originalPixel;
    //[SerializeField] public string fieldSize;
    private string fieldSize;
    public GridPixelScript[,] grid;
	private int gridRows = 10;
    private int gridCols = 10;
    private float offsetX;
    private float offsetY;

    public float OffsetX { get => offsetX; set => offsetX = value; }
    public float OffsetY { get => offsetY; set => offsetY = value; }



    // Start is called before the first frame update
    void Start()
    {
        fieldSize = "small";
	switch(fieldSize)
	{
		case "small":
			gridRows = 10;
			gridCols = 10;
		break;
		case "medium":
			gridRows = 50;
			gridCols = 50;
		break;
		case "big":
			gridRows = 100;
			gridCols = 100;
		break;
		case "enormous":
			gridRows = 500;
			gridCols = 500;
		break;
        default:
            gridRows = 10;
            gridCols = 10;
        break;
        }
        grid = new GridPixelScript[gridRows, gridCols];
        Vector3 startPos = originalPixel.transform.position;

        offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
		for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
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
}
