using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrezenheimGameController : MonoBehaviour
{
    [SerializeField] public GridPixel originalPixel;
    private GridPixel[][] grid;
    public const int gridRows = 10;
    public const int gridCols = 10;
    private float offsetX;
    private float offsetY;
    // Start is called before the first frame update
    void Start()
    {
        grid = new GridPixel[gridRows][];
        for(int i=0;i<gridRows;i++)
        {
            grid[i] = new GridPixel[gridCols];
        }
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
                //GridPixel pixel;
                if (i == 0 && j == 0)
                {
                    //pixel = originalPixel;
                    grid[i][j] = originalPixel;
                }
                else
                {
                    grid[i][j] = Instantiate(originalPixel) as GridPixel;
                    //pixel = Instantiate(originalPixel) as GridPixel;
                }
                //int id = Random.Range(0, images.Length);
                float posX = (offsetX * j) + startPos.x;
                float posY = -(offsetY * i) + startPos.y;
                grid[i][j].transform.position = new Vector3(posX, posY, startPos.z);
                //pixel.transform.position = new Vector3(posX, posY, startPos.z);
                //grid[i][j] = pixel;
            }
        }
        drawLine(4,5, 9, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void drawLine(int Ax, int Ay, int Bx, int By)
        {
        int ax = Ax;
        int ay = Ay;
        int bx = Bx;
        int by = By;
            grid[ax][ay].GetComponent<GridPixel>().setPixelState(true);
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

                        try
                        {
                        grid[t][y].GetComponent<GridPixel>().setPixelState(true);
                        //bitmap.SetPixel(t, y, color);
                    }
                        catch (System.ArgumentOutOfRangeException e)
                        {

                        }
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
                        try
                        {
                        grid[t][y].GetComponent<GridPixel>().setPixelState(true);
                        //bitmap.SetPixel(t, y, color);
                    }
                        catch (System.ArgumentOutOfRangeException e)
                        {

                        }

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
                        try
                        {
                        grid[y][t].GetComponent<GridPixel>().setPixelState(true);
                        //bitmap.SetPixel(y, t, color);
                    }
                        catch (System.ArgumentOutOfRangeException e)
                        {

                        }
                        //Point tmp = new Point(y, t);
                        //curve.Add(tmp);
                    }
                } 

            }
           
                 grid[bx][by].GetComponent<GridPixel>().setPixelState(true);
            //bitmap.SetPixel(B.X, B.Y, color);
              
           

            //pictureBox1.Refresh();
        }
    private void Swap (ref int a, ref int b)
    {
        int c = a;
        a = b;
        b = c;
    }
    /*#include <iostream>
using namespace std;
void Brezenhem(char **z, int x0, int y0, int x1, int y1)
{
 int A, B, sign;
 A = y1 - y0;
 B = x0 - x1;
 if (abs(A) > abs(B)) sign = 1;
 else sign = -1;
 int signa, signb;
 if (A < 0) signa = -1;
 else signa = 1;
 if (B < 0) signb = -1;
 else signb = 1;
 int f = 0;
 z[y0][x0] = '*';
 int x = x0, y = y0;
 if (sign == -1) 
 {
   do {
     f += A*signa;
     if (f > 0)
     {
       f -= B*signb;
       y += signa;
     }
     x -= signb;
     z[y][x] = '*';
   } while (x != x1 || y != y1);
 }
 else
 {
   do {
     f += B*signb;
     if (f > 0) {
       f -= A*signa;
       x -= signb;
     }
     y += signa;
     z[y][x] = '*';
   } while (x != x1 || y != y1);
 }
}
int main()
{
 const int SIZE = 25; // размер поля
 int x1, x2, y1, y2;
 char **z;
 z = new char*[SIZE];
 for (int i = 0; i < SIZE; i++) 
 {
   z[i] = new char[SIZE];
   for (int j = 0; j < SIZE; j++)
     z[i][j] = '-';
 }
 cout << "x1 = ";     cin >> x1;
 cout << "y1 = ";     cin >> y1;
 cout << "x2 = ";     cin >> x2;
 cout << "y2 = ";    cin >> y2;
 Brezenhem(z, x1, y1, x2, y2);
 for (int i = 0; i < SIZE; i++) 
 {
   for (int j = 0; j < SIZE; j++)
     cout << z[i][j];
   cout << endl;
 }
 cin.get(); cin.get();
 return 0;
}*/
}
