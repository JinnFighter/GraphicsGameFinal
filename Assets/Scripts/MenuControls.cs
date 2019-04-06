using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public const string path = "Assets/Data/players_base.xml";
    // Start is called before the first frame update
    void Start()
    {
        PlayerProfilesContainer container = PlayerProfilesContainer.Load(path);

        foreach(PlayerProfile profile in container.profiles)
        {
            Debug.Log(profile.name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorPickerButtonPressed()
    {
        SceneManager.LoadScene("ColorPicker");
    }
    public void BrezenheimButtonPressed()
    {
        SceneManager.LoadScene("Brezenheim");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
