using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TurtleGameController : MonoBehaviour
{
    [SerializeField] private Pixel originalPixel;
    [SerializeField] private Turtle turtle;
    [SerializeField] private InputField routeInputField;

    
    
    private TurtleGameMode _gameMode;
    // Start is called before the first frame update
    void Start()
    {
        var gameField = GetComponent<GameField>();
        _gameMode = new TurtleGameMode(originalPixel, turtle, routeInputField, GetComponent<GameplayTimer>(), gameField, gameField.Difficulty);
    }

    

    public void SendStartGameEvent()
    {
        var pfManager = GetComponent<ProfilesManager>();
        if (PlayerPrefs.GetInt(pfManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit") == 1)
        {
            PlayerPrefs.SetInt(pfManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit", 0);
            PlayerPrefs.Save();
            Messenger.Broadcast(GameEvents.START_GAME);
        }
    }
}