using System.Collections.Generic;
using UnityEngine;
using System;

public class BezierGameController : MonoBehaviour
{
    private BezierGameMode _gameMode;
    // Start is called before the first frame update
    void Start()
    {
        _gameMode = new BezierGameMode(GetComponent<GameplayTimer>(), GetComponent<GameField>().Difficulty, GetComponent<GameField>());
    }
}
