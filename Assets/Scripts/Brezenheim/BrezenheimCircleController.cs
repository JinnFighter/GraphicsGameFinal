using UnityEngine;

public class BrezenheimCircleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
    }

    public void gameCheck(GridPixelScript invoker)
    {

    }

    public void Brezenheim4Circle(int Xc, int Yc, int r)
    {
        var xc = Yc;
        var yc = Xc;
        int x, y, d;
        x = 0;
        y = r;
        d = 3 - 2 * y;
        while (x <= y)
        {
            var gameField = GetComponent<GameField>();
            gameField.grid[x + xc, y + yc].setPixelState(true);
            gameField.grid[x + xc, -y + yc].setPixelState(true);
            gameField.grid[-x + xc, -y + yc].setPixelState(true);
            gameField.grid[-x + xc, y + yc].setPixelState(true);
            gameField.grid[y + xc, x + yc].setPixelState(true);
            gameField.grid[y + xc, -x + yc].setPixelState(true);
            gameField.grid[-y + xc, -x + yc].setPixelState(true);
            gameField.grid[-y + xc, x + yc].setPixelState(true);

            if (d < 0)
                d = d + 4 * x + 6;
            else
            {
                d = d + 4 * (x - y) + 10;
                y--;
            }
            x++;
        }
    }
}
