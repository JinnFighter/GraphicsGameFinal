using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    private ProfilesManager pfManager;
    // Start is called before the first frame update
    void Start()
    {
        pfManager = GetComponent<ProfilesManager>();
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
    public void LoadProfiles(GameObject panel)
    {
        if(pfManager.Container.profiles.Count!=0)
        {
            
            foreach (PlayerProfile profile in pfManager.Container.profiles)
            {
                GameObject go = new GameObject();
                go.AddComponent<Button>();
                Button btn = go.GetComponent<Button>();
                btn.GetComponent<Text>().text = profile.name;
                btn.transform.SetParent(panel.transform);
                Debug.Log(profile.name + " " + profile.active);
            }
        }
        
    }
}
