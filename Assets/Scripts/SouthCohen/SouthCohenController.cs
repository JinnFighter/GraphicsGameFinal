using UnityEngine;
using UnityEngine.SceneManagement;

public class SouthCohenController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer border;

    private SouthCohenGameMode _gameMode;

    // Start is called before the first frame update
    void Start()
    {
        var gameField = GetComponent<GameField>();
        _gameMode = new SouthCohenGameMode(GetComponent<GameplayTimer>(), border, gameField, gameField.Difficulty);
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