using UnityEngine;
using UnityEngine.UI;

public class BrezenheimGameModeController : GameModeController
{
    [SerializeField] private InputField textField;

    void Awake()
    {
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, Check);
    }

    void Start()
    {
        var gameField = GetComponent<GameField>();
        GameMode = new NewBrezenheimGameMode(gameField.Difficulty, gameField, textField);
    }

    public void Check(Pixel invoker) 
    {
        if(GameMode.IsActive())
            GameMode.Check(invoker);
    }

    void OnDestroy()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, Check);
    }
}
