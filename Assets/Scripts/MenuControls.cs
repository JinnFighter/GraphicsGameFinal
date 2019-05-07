using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    private ProfilesManager pfManager;
    private bool loadedProfiles;
    private List<GameObject> profileButtons;
   [SerializeField] private GameObject template;
    [SerializeField] private Text noProfilesText;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject createProfilePage;
    [SerializeField] private Button loginButton;
    // Start is called before the first frame update
    void Start()
    {
        loadedProfiles = false;
        profileButtons = new List<GameObject>();
        //template = new GameObject();
        pfManager = GetComponent<ProfilesManager>();
        if(pfManager.ActiveProfile!=null)
        {

            loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
        }
        
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
        if(loadedProfiles)
        {
            return;
        }
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
                /*GameObject button = new GameObject();
                button.AddComponent<RectTransform>();
                button.transform.SetParent(panel.transform);
                button.GetComponent<RectTransform>().sizeDelta = template.GetComponent<RectTransform>().rect.size;
                button.AddComponent<Button>();
                button.GetComponent<Button>().onClick.AddListener(() => { SetActiveProfile(profile.name); }
                );*/
                GameObject btn = Instantiate(template) as GameObject;
                btn.GetComponentInChildren<Text>().text = profile.name;
                btn.transform.parent = panel.transform;
                btn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                //btn.GetComponent<Button>().onClick.RemoveAllListeners();
                btn.GetComponent<Button>().onClick.AddListener(
                 () => { SetActiveProfile(profile.name); }
                );
                btn.transform.localScale = template.transform.localScale;
                profileButtons.Add(btn);
                
            }
            loadedProfiles = true;
        }
        else
        {
            noProfilesText.gameObject.SetActive(true);

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
        loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
        loginPage.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        //if(profile.name == profileName)
        // {
        //  profile.active = true;
        // pfManager.ActiveProfile = profile;
        //Debug.Log(profile.name + " is now active profile!");
        //break;
        //}
    }
    public void SubmitNewProfile(InputField field)
    {
        if(field.text=="")
        {
            Debug.Log("Enter name!");
        }
        else
        {
            
            foreach (PlayerProfile profile in pfManager.Container.profiles)
            {
                if(profile.name == field.text)
                {
                    Debug.Log("The profile with this name already exists!");
                    return;
                }
            }
            PlayerProfile p = new PlayerProfile(field.text, false);
            pfManager.Container.profiles.Add(p);
            SetActiveProfile(field.text);
            pfManager.Container.Save(ProfilesManager.path);
            foreach (PlayerProfile profile in pfManager.Container.profiles)
            {
                
                    Debug.Log(profile.name);
                
            }
            loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
            foreach (GameObject obj in profileButtons)
            {
                Destroy(obj);
            }
            profileButtons.Clear();
            if(noProfilesText.gameObject.activeSelf)
            {
                noProfilesText.gameObject.SetActive(false);
            }
            loadedProfiles = false;
            loginPage.gameObject.SetActive(false);
            createProfilePage.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
    }
}
