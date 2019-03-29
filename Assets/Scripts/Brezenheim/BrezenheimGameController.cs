using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class BrezenheimGameController : MonoBehaviour
{
    [SerializeField] public GridPixelScript originalPixel;
    private  GridPixelScript[,] grid;
    private List<GridPixelScript> linePoints;
    private List<int> ds;
    private GridPixelScript last_point;
    private GridPixelScript prev_point;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;
    private int iteration;
    [SerializeField]private InputField textField;


    // Start is called before the first frame update
    void Start()
    {
        ds = new List<int>(1);
        linePoints = new List<GridPixelScript>(1);
        //grid = new GridPixelScript[gridRows][];
        //for(int i=0;i<gridRows;i++)
        //{
        //   grid[i] = new GridPixelScript[gridCols];
        //}
        grid = new GridPixelScript[gridRows, gridCols];
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
                GridPixelScript pixel;
                
                if (i == 0 && j == 0)
                {
                    pixel = originalPixel;
                    //grid[i,j] = originalPixel;
                }
                else
                {
                    //grid[i,j] = Instantiate(originalPixel) as GridPixelScript;
                    pixel = Instantiate(originalPixel) as GridPixelScript;
                    float posX = (offsetX * j) + startPos.x;
                    float posY = -(offsetY * i) + startPos.y;
                    pixel.transform.position = new Vector3(posX, posY, startPos.z);
                }
                //int id = Random.Range(0, images.Length);

                //pixel.setPixelState(true);
                //grid[i,j].transform.position = new Vector3(posX, posY, startPos.z);

                pixel.transform.SetParent(this.transform.Find("Canvas").transform);
                //pixel.setPixelState(true);
                grid[i,j] = pixel;
               
                //Debug.Log(grid[i, j].GetHashCode().ToString());
            }
        }
        //drawLine(4,5,9,9);
        //Bresenham4Line(4, 5, 8, 0);
        Bresenham4Line(4, 5, 9, 9);
        last_point = linePoints[linePoints.Count-1];
        iteration = 0;
        prev_point = linePoints[0];
        textField.text = ds[0].ToString();
        grid[5,4].setPixelState(true);
        grid[9,9].setPixelState(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drawLine(int Ax, int Ay, int Bx, int By)
    {
        int ax = Ax;
        int ay = Ay;
        int bx = Bx;
        int by = By;
        //grid[ax][ay].pixel_empty.SetActive(false);
        grid[ax, ay].GetComponent<GridPixelScript>().setPixelState(true);
        //bitmap.SetPixel(A.X, A.Y, color);


         int dx = Math.Abs(bx - ax);//разница между иксами первой точки и второй;
          int dy = Math.Abs(by - ay);//разница между игреками первой точки и второй;
          int d = (dy << 1) - dx;//разность между двойной разницей игреков и разницей иксов
          int d1 = dy << 1;//умножение разницы игреков на два;
          int d2 = (dy - dx) << 1;//умножение разности разниц игреков и иксов на два;

          if (Math.Abs(dy) < Math.Abs(dx))
          {
              //Swap(ref A,ref B);
              if (ax <= bx)
              {

                  int y = ay;//стартовая точка для y;
                  for (int t = ax; t <= bx; t++)
                  {
                      if (d > 0)
                      {
                          d += d2;
                          if (ay <= by)
                          {
                              y++;
                          }
                          else
                          {
                              y--;
                          }
                      }
                      else
                      {
                          d += d1;
                      }


                      grid[t,y].GetComponent<GridPixelScript>().setPixelState(true);
                      //bitmap.SetPixel(t, y, color);


                      //this.bitmap.SetPixel(t, y, color);
                      //Point tmp = new Point(t, y);
                      //curve.Add(tmp);
                  }
              }
              else
              {
                  Swap(ref ax, ref bx);
              Swap(ref ay, ref by);
              int y = ay;//стартовая точка для y;
                  for (int t = ax; t <= bx; t++)
                  {
                      if (d < 0)
                      {
                          d -= d2;
                          if (ay <= by)
                          {
                              y++;
                          }
                          else
                          {
                              y--;
                          }
                      }
                      else
                      {
                          d -= d1;
                      }

                      grid[t,y].GetComponent<GridPixelScript>().setPixelState(true);
                      //bitmap.SetPixel(t, y, color);



                      //Point tmp = new Point(t, y);
                      //curve.Add(tmp);
                  }

              }

          }
          else
          {
              d = (dx << 1) - dy;//разность между двойной разницей игреков и разницей иксов(наоборот)
              d1 = dx << 1;//умножение разницы игреков на два(наоборот);
              d2 = (dx - dy) << 1;//умножение разности разниц игреков и иксов на два(наоборот);
              if (ay<= by)
              {
                  int y = ax;//стартовая точка для y;
                  for (int t = ay; t <= by; t++)
                  {
                      if (d > 0)
                      {
                          d += d2;
                          if (ax <= bx)
                          {
                              y++;
                          }
                          else
                          {
                              y--;
                          }
                      }
                      else
                      {
                          d += d1;
                      }

                  Debug.Log("X: " + t + " y: " + y);
                      grid[y,t].GetComponent<GridPixelScript>().setPixelState(true);
                      //bitmap.SetPixel(y, t, color);


                      //Point tmp = new Point(y, t);
                      //curve.Add(tmp);
                  }
              }
              else
              {
              Swap(ref ax, ref bx);
              Swap(ref ay, ref by);
              int y = ax;//стартовая точка для y;
                  for (int t = ay; t <= by; t++)
                  {
                      if (d < 0)
                      {
                          d -= d2;
                          if (ax <= bx)
                          {
                              y++;
                          }
                          else
                          {
                              y--;
                          }
                      }
                      else
                      {
                          d -= d1;
                      }

                      grid[y,t].GetComponent<GridPixelScript>().setPixelState(true);
                      //bitmap.SetPixel(y, t, color);


                      //Point tmp = new Point(y, t);
                      //curve.Add(tmp);
                  }
              } 

          }
          

        //grid[bx][by].GetComponent<GridPixel>().setPixelState(true);
        grid[Bx, By].GetComponent<GridPixelScript>().setPixelState(true);
        //bitmap.SetPixel(B.X, B.Y, color);



        //pictureBox1.Refresh();
    }
    public void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }
    public void Bresenham4Line( int X0, int Y0, int X1, int Y1)
        {
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

            //linePoints.Add(grid[x0, y0]);
            ds.Add(d);
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

                linePoints.Add(grid[x, y]);
                ds.Add(d);
                //PutPixel(g, clr, x, y, 255);
                x +=sx;
                
            }
            }
            else
            {
                int d = (dx << 1) - dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

            //grid[x0, y0].GetComponent<GridPixelScript>().setPixelState(true);

            //linePoints.Add(grid[x0, y0]);
            ds.Add(d);
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

                linePoints.Add(grid[x, y]);
                ds.Add(d);
                //PutPixel(g, clr, x, y, 255);
                y +=sy;
                
            }
            }
        }
    public void gameCheck(GridPixelScript invoker)
    {
        if(prev_point == last_point)
        {
            Debug.Log("Enough, start over, it's finished!");
            return;
        }
        else
        {
            prev_point = linePoints[iteration];
            
            if(invoker == prev_point)
            {
                Debug.Log("Correct!");
                invoker.setPixelState(true);//changle later to true! IMPORTANT!!!
                iteration++;
                textField.text = ds[iteration].ToString();
                prev_point = linePoints[iteration];
            }
            else
            {
                Debug.Log("Wrong!");
            }
            
        }
    }

}
