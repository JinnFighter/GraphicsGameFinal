using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrezenheimGameController : MonoBehaviour
{
    [SerializeField] public GridPixelScript originalPixel;
    public  GridPixelScript[,] grid;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;


    // Start is called before the first frame update
    void Start()
    {
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
        drawLine(4,5, 0, 0);
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


        /*  int dx = Math.Abs(bx - ax);//разница между иксами первой точки и второй;
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


                      grid[t][y].GetComponent<GridPixel>().setPixelState(true);
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

                      grid[t][y].GetComponent<GridPixel>().setPixelState(true);
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
                      grid[y][t].GetComponent<GridPixel>().setPixelState(true);
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

                      grid[y][t].GetComponent<GridPixel>().setPixelState(true);
                      //bitmap.SetPixel(y, t, color);


                      //Point tmp = new Point(y, t);
                      //curve.Add(tmp);
                  }
              } 

          }
          */

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
}
