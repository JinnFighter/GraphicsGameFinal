using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : MonoBehaviour
{
	[SerializeField] public Pixel originalPixel;
    public Pixel[,] grid;
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }
    public int Height { get; set; } = 10;
    public int Width { get; set; } = 10;
    public int Difficulty { get; set; }

    void Awake()
    {
        Difficulty = PlayerPrefs.GetInt("difficulty");
        switch(Difficulty)
        {
            case 1:
                Height = 15;
                Width = 15;
                originalPixel.transform.localScale = new Vector3(12, 12, 1);
                break;
            case 2:
                Height = 20;
                Width = 20;
                originalPixel.transform.localScale = new Vector3(10, 10, 1);
                break;
            default:
                Height = 10;
                Width = 10;
                originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
        }
        grid = new Pixel[Height, Width];
        var startPos = originalPixel.transform.position;
        var boundsSize = originalPixel.GetComponent<SpriteRenderer>().bounds.size;
        OffsetX = boundsSize.x;
        OffsetY = boundsSize.y;
		for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Pixel pixel;
                
                if (i == 0 && j == 0)
                    pixel = originalPixel;
                else
                {    
                    pixel = Instantiate(originalPixel);
                    float posX = (OffsetX * j) + startPos.x;
                    float posY = -(OffsetY * i) + startPos.y;
                    pixel.transform.position = new Vector3(posX, posY, startPos.z);
                }

                pixel.Position = new Position(i, j);
                grid[i, j] = pixel;  
            }
        }
    }

    public void ClearGrid()
    {
        foreach(var pixel in grid.Cast<Pixel>().Where(px => px.IsActive()))
            pixel.SetState(false);
    }

    public Pixel GetPixel(int i, int j) => grid[i, j];

    public bool Contains(Position position) => position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;

    public void Draw(List<Position> positions)
    {
        foreach(var position in positions)
        {
            if (Contains(position))
                grid[(int)position.X, (int)position.Y].SetState(true);
        }
    }
}