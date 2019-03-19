using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrezenheimGameController : MonoBehaviour
{
    [SerializeField] public GridPixel originalPixel;
    public const int gridRows = 10;
    public const int gridCols = 10;
    public const float offsetX = 0.5f;
    public const float offsetY = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalPixel.transform.position;

        
        /* for(int i=0;i<numbers.Length;i++)
         {
             Debug.Log(numbers[i] + " ");
         }*/
        for (int i = 0; i < gridRows; i++)
        {
            for (int j = 0; j < gridCols; j++)
            {
                GridPixel pixel;
                if (i == 0 && j == 0)
                {
                    pixel = originalPixel;
                }
                else
                {
                    pixel = Instantiate(originalPixel) as GridPixel;
                }

                

                //int id = Random.Range(0, images.Length);
                float posX = (offsetX * j) + startPos.x;
                float posY = -(offsetY * i) + startPos.y;
                pixel.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
