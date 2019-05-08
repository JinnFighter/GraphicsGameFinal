using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
        Messenger.Broadcast(GameEvents.PAUSE_GAME);
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        Messenger.Broadcast(GameEvents.CONTINUE_GAME);
        this.gameObject.SetActive(false);
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
