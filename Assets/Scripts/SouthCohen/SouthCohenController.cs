using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthCohenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameField gameField = gameObject.GetComponent<GameField>();
        gameObject.GetComponent<Algorithms>().southCohen(gameField.grid[0, 0], gameField.grid[9, 9], 
            gameField.grid[4, 4], gameField.grid[8, 8]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
