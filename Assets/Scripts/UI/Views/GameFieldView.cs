using UnityEngine;

public class GameFieldView : MonoBehaviour, IGameFieldView
{
    [SerializeField] private Pixel _originalPixel;
    private Pixel[,] _grid;

    public void GenerateField(int difficulty, int width, int height)
    {
        switch (difficulty)
        {
            case 1:
                _originalPixel.transform.localScale = new Vector3(12, 12, 1);
                break;
            case 2:
                _originalPixel.transform.localScale = new Vector3(10, 10, 1);
                break;
            default:
                _originalPixel.transform.localScale = new Vector3(20, 20, 1);
                break;
        }
        _grid = new Pixel[height, width];
        var startPos = _originalPixel.transform.position;
        var boundsSize = _originalPixel.GetComponent<SpriteRenderer>().bounds.size;
        var offsetX = boundsSize.x;
        var offsetY = boundsSize.y;
		for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                Pixel pixel;
                
                if (i == 0 && j == 0)
                    pixel = _originalPixel;
                else
                {    
                    pixel = Instantiate(_originalPixel);
                    float posX = (offsetX * j) + startPos.x;
                    float posY = -(offsetY * i) + startPos.y;
                    var transform = pixel.transform;
                    transform.position = new Vector3(posX, posY, startPos.z);
                    transform.SetParent(this.transform);
                }

                pixel.Position = new Position(i, j);
                _grid[i, j] = pixel;  
            }
        }
    }
}
