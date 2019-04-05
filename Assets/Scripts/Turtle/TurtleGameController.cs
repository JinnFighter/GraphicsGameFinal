using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] public TurtleGridPixelScript originalPixel;
    private TurtleGridPixelScript[,] grid;
    [SerializeField] public InputField routeInputField;
    [SerializeField] public Turtle turtle;
    private string route;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;
    private enum commandsEnum {FORWARD, ROTATE_PLUS, ROTATE_MINUS};
    private Hashtable commandsTable = new Hashtable { {'F', commandsEnum.FORWARD}, { '+', commandsEnum.ROTATE_PLUS },
        { '-', commandsEnum.ROTATE_MINUS } };
    private int angle = 90;
    // Start is called before the first frame update
    void Start()
    {
        route = "FFF-FF";
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
                grid[i, j] = pixel;
            }
        }
        routeInputField.text = route;
        executeMoveSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GameChecker()
    {

    }
    void executeMoveSequence()
    {
        for(int i=0;i<route.Length;i++)
        {
            switch(route[i])
            {
                case 'F':
                    grid[turtle.x, turtle.y].setPixelState(true);
                    turtle.moveForward();
                    break;
                case '+':
                    turtle.rotateLeft();
                    break;
                case '-':
                    turtle.rotateRight();
                    break;
            }
        }
    }
}
