using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    private ProfilesManager pfManager;
   [SerializeField] private GameObject template;
    // Start is called before the first frame update
    void Start()
    {
        //template = new GameObject();
        pfManager = GetComponent<ProfilesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorPickerButtonPressed()
    {
        //GameObject loadingScreen = new GameObject();
        //loadingScreen.AddComponent<Sprite>();
        //this.gameObject.SetActive(true);
        //GetComponent<SceneLoader>().LoadScene("ColorPicker");
        //SceneManager.LoadSceneAsync("ColorPicker");
        //SceneManager.LoadScene("ColorPicker");
    }
    public void BrezenheimButtonPressed()
    {
        //SceneManager.LoadScene("Brezenheim");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
    public void LoadProfiles(GameObject panel)
    {
        if (pfManager.Container.profiles.Count != 0)
        {

           //for(int i=0;i<pfManager.Container.profiles.Count;i++)
            //{
               // GameObject btn = Instantiate(template) as GameObject;
                //btn.GetComponentInChildren<Text>().text = pfManager.Container.profiles[i].name;
                //btn.GetComponent<Button>().onClick.AddListener(
                   // () => { SetActiveProfile(pfManager.Container.profiles[i].name); }
                    //);
                //btn.transform.parent = panel.transform;
            //}
            foreach(PlayerProfile profile in pfManager.Container.profiles)
            {
                GameObject btn = Instantiate(template) as GameObject;
                btn.GetComponentInChildren<Text>().text = profile.name;
                btn.GetComponent<Button>().onClick.AddListener(
                 () => { SetActiveProfile(profile.name); }
                );
                btn.transform.parent = panel.transform;
            }
        }

    }
    public void SetActiveProfile(string profileName)
    {
        //Debug.Log(profileName);
        foreach (PlayerProfile profile in pfManager.Container.profiles)
        {
            if(profile.active)
            {
                profile.active = false;
                //pfManager.ActiveProfile = profile;
                //Debug.Log(profile.name + " is now active profile!");
                break;
            }
        }
        foreach (PlayerProfile profile in pfManager.Container.profiles)
        {
            if (profile.name == profileName)
            {
                profile.active = true;
                pfManager.ActiveProfile = profile;
                //Debug.Log(profile.name + " is now active profile!");
                break;
            }
        }
        pfManager.Container.Save(ProfilesManager.path);
        //if(profile.name == profileName)
        // {
        //  profile.active = true;
        // pfManager.ActiveProfile = profile;
        //Debug.Log(profile.name + " is now active profile!");
        //break;
        //}
    }
}
