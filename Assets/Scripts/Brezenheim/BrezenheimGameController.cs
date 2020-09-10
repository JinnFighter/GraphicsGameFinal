using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrezenheimGameController : MonoBehaviour
{
    [SerializeField] private InputField textField;
    private BrezenheimGameMode _gameMode;

    // Start is called before the first frame update
    void Start()
    {
        _gameMode = new BrezenheimGameMode(GetComponent<GameplayTimer>(), GetComponent<GameField>().Difficulty, GetComponent<GameField>(), textField);
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
