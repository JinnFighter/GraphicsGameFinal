using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public TurtleGridPixelScript originalPixel;
    private TurtleGridPixelScript[,] grid;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        grid = new TurtleGridPixelScript[gridRows, gridCols];
        Vector3 startPos = originalPixel.transform.position;

        offsetX = originalPixel.GetComponent<SpriteRenderer>().bounds.size.x;
        offsetY = originalPixel.GetComponent<SpriteRenderer>().bounds.size.y;
        /* for(int i=0;i<numbers.Length;i++)
         {
             Debug.Log(numbers[i] + " ");
         }*/
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
            {
                TurtleGridPixelScript pixel;

                if (i == 0 && j == 0)
                {
                    pixel = originalPixel;
                    //grid[i,j] = originalPixel;
                }
                else
                {
                    //grid[i,j] = Instantiate(originalPixel) as GridPixelScript;
                    pixel = Instantiate(originalPixel) as TurtleGridPixelScript;
                    float posX = (offsetX * j) + startPos.x;
                    float posY = -(offsetY * i) + startPos.y;
                    pixel.transform.position = new Vector3(posX, posY, startPos.z);
                }
                //int id = Random.Range(0, images.Length);

                //pixel.setPixelState(true);
                //grid[i,j].transform.position = new Vector3(posX, posY, startPos.z);

                //pixel.transform.SetParent(this.transform.Find("Canvas").transform);
                //pixel.setPixelState(true);
                grid[i, j] = pixel;

                //Debug.Log(grid[i, j].GetHashCode().ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
