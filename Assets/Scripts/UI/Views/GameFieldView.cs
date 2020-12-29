using UnityEngine;
using UnityEngine.UI;

public class GameFieldView : MonoBehaviour, IGameFieldView
{
    [SerializeField] private Pixel _originalPixel;
    private Pixel[,] _grid;

    public delegate void Check(Pixel invoker);

    public event Check GameCheckEvent;

    public void GameCheck(Pixel invoker) => GameCheckEvent?.Invoke(invoker);

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
        var boundsSize = _originalPixel.GetComponent<SpriteRenderer>().bounds.size;
        var layoutGroup = GetComponent<GridLayoutGroup>();
        layoutGroup.spacing = new Vector2(boundsSize.x, boundsSize.y);
        layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = width;
		for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                Pixel pixel;
                
                if (i == 0 && j == 0)
                    pixel = _originalPixel;
                else
                {    
                    pixel = Instantiate(_originalPixel);
                    pixel.transform.SetParent(_originalPixel.transform.parent, false);
                }

                pixel.Position = new Position(i, j);
                _grid[i, j] = pixel;  
            }
        }
    }

    public void SetState(int x, int y, bool state) => _grid[x, y].SetState(state);
}
