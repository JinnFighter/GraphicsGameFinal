using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class MultipleBrezenheimController : MonoBehaviour
{
    private List<GridPixelScript>[] linePoints;
    private List<int>[] ds;
    private GridPixelScript last_point;
    private GridPixelScript prev_point;
    private int cur_line;
    private int iteration;
    [SerializeField] private InputField textField;
    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<GridPixelScript>[2];
        ds = new List<int>[2];
        for (int i = 0; i < 2; i++)
        {
            linePoints[i] = new List<GridPixelScript>();
            ds[i] = new List<int>();
        }
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Bresenham4Polygon(0, 0, 4, 5, 0);
        Bresenham4Polygon(4, 5, 8, 0, 1);
        last_point = linePoints[0][linePoints[0].Count - 1];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Bresenham4Polygon(int X0, int Y0, int X1, int Y1, int j)
    {
        GameField gameField = gameObject.GetComponent("GameField") as GameField;
        if(gameField.grid==null)
        {
            Debug.Log("gameField failed");
        }
        int x0 = Y0;
        int y0 = X0;
        int x1 = Y1;
        int y1 = X1;
        //Изменения координат
        int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        int sx = (x1 >= x0) ? (1) : (-1);
        int sy = (y1 >= y0) ? (1) : (-1);

        if (dy < dx)
        {
            int d = (dy << 1) - dx;
            int d1 = dy << 1;
            int d2 = (dy - dx) << 1;

            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);
            //GameField a = gameObject.GetComponent("GameField") as GameField;
            //a.grid[x0, y0].setPixelState(true);
            gameField.grid[x0, y0].setPixelState(true);
            //field.grid[x0, y0].setPixelState(true);
            //linePoints.Add(grid[x0, y0]);
            ds[j].Add(d);
            //PutPixel(g, clr, x0, y0, 255);
            int x = x0 + sx;
            int y = y0;
            for (int i = 1; i <= dx; i++)
            {
                //Debug.Log(d.ToString());
                //ds.Add(d);
                if (d > 0)
                {
                    d += d2;
                    y += sy;

                }
                else
                    d += d1;

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);


                //linePoints.Add(grid[x, y]);
                //linePoints.Add(GetComponent<GameField>().grid[x,y]);
                //linePoints[j].Add(GameObject.Find("GameController").GetComponent<GameField>().grid[x, y]);
;                linePoints[j].Add(gameField.grid[x, y]);
                ds[j].Add(d);
                //PutPixel(g, clr, x, y, 255);
                x += sx;

            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;

            gameField.grid[x0, y0].setPixelState(true);
            //field.grid[x0,y0].setPixelState(true);
            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);

            //linePoints.Add(grid[x0, y0]);
            ds[j].Add(d);
            //PutPixel(g, clr, x0, y0, 255);
            int x = x0;
            int y = y0 + sy;
            for (int i = 1; i <= dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    x += sx;
                }
                else
                    d += d1;

                //grid[x, y].GetComponent<GridPixelScript>().setPixelState(true);

                //linePoints.Add(grid[x, y]);
                linePoints[j].Add(GetComponent<GameField>().grid[x, y]);
                ds[j].Add(d);
                //PutPixel(g, clr, x, y, 255);
                y += sy;

            }
        }
        last_point = linePoints[j][linePoints[j].Count - 1];
        last_point.setPixelState(true);
        //List<GridPixelScript> temp = linePoints[linePoints.Length - 1];
        //last_point = temp[temp.Count - 1];
        //temp[temp.Count - 1].setPixelState(true);
        //linePoints[linePoints.Count - 1].setPixelState(true);
    }

    public void gameCheck(GridPixelScript invoker)
    {
        if (prev_point == last_point)
        {
            if(cur_line==linePoints.Length-1)
            {
                Debug.Log("Enough, start over, it's finished!");
                return;
            }
            else
            {
                cur_line++;
                iteration = 0;
                last_point = linePoints[cur_line][linePoints[cur_line].Count - 1];
            }
           
        }
        else
        {
            prev_point = linePoints[cur_line][iteration];

            if (invoker == prev_point)
            {
                Debug.Log("Correct!");
                invoker.setPixelState(true);//changle later to true! IMPORTANT!!!
                iteration++;
                textField.text = ds[cur_line][iteration].ToString();
                prev_point = linePoints[cur_line][iteration];
            }
            else
            {
                Debug.Log("Wrong!");
            }

        }
    }
}
