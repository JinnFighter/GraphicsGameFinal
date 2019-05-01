using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthCohenController : MonoBehaviour
{
    private GridPixelScript[] borderPoints;
    private int borderWidth;
    private int borderHeight;
    [SerializeField] private SpriteRenderer border;
    // Start is called before the first frame update
    void Start()
    {
        borderWidth = 5;
        borderHeight = 5;
        borderPoints = new GridPixelScript[2];
        GameField gameField = gameObject.GetComponent<GameField>();
        borderPoints[0] = gameField.grid[4, 4];
        borderPoints[1] = gameField.grid[8, 8];
        Vector3 pos = borderPoints[0].transform.position;
        pos.x += 12.5f;
        pos.y -= 12.5f;
        pos.z = border.transform.position.z;
        border.transform.position = pos;
        Vector3 scale = border.transform.localScale;
        scale.x = (scale.x) * 10;
        scale.y = (scale.y ) * 10;
        border.transform.localScale = scale;
        
        //border.transform.localScale.Set(border.transform.localScale.x + borderWidth, border.transform.localScale.y + borderHeight, border.transform.localScale.z);
        //border.sprite.rect.size.Set(border.sprite.rect.size.x*borderWidth, border.sprite.rect.size.y*borderHeight);
        gameObject.GetComponent<Algorithms>().southCohen(gameField.grid[0, 0], gameField.grid[9, 9], 
            gameField.grid[4, 4], gameField.grid[8, 8]);
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameCheck(GridPixelScript invoker)
    {
        invoker.setPixelState(true);
    }
}
