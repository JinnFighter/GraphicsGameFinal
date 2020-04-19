using System.Collections.Generic;
using UnityEngine;
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
        if(pfManager.Container.profiles.Count == 0)
        {
            noProfilesText.gameObject.SetActive(true);
            loginPage.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        if(pfManager.ActiveProfile != null)
            loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
    }

    public void ColorPickerButtonPressed()
    {

    }

    public void BrezenheimButtonPressed()
    {

    }

    public void ExitPressed() => Application.Quit();

    public void LoadProfiles(GameObject panel)
    {
        if(loadedProfiles) return;

        if (pfManager.Container.profiles.Count != 0)
        {
            foreach(var profile in pfManager.Container.profiles)
            {
                var btn = Instantiate(template) as GameObject;
                btn.GetComponentInChildren<Text>().text = profile.name;
                btn.transform.parent = panel.transform;
                btn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                btn.GetComponent<Button>().onClick.AddListener(
                 () => { SetActiveProfile(profile.name); }
                );
                btn.transform.localScale = template.transform.localScale;
                profileButtons.Add(btn);
            }
            loadedProfiles = true;
        }
        else
            noProfilesText.gameObject.SetActive(true);
    }

    public void SetActiveProfile(string profileName)
    {
        foreach (var profile in pfManager.Container.profiles)
        {
            if(profile.active)
            {
                profile.active = false;
                break;
            }
        }

        foreach (var profile in pfManager.Container.profiles)
        {
            if (profile.name == profileName)
            {
                profile.active = true;
                pfManager.ActiveProfile = profile;
                break;
            }
        }

        pfManager.Container.Save(ProfilesManager.path);
        loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
        loginPage.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }

    public void SubmitNewProfile(InputField field)
    {
        if(field.text == "")
            Debug.Log("Enter name!");
        else
        { 
            foreach (var profile in pfManager.Container.profiles)
            {
                if(profile.name == field.text) return;
            }

            var p = new PlayerProfile(field.text, false);
            pfManager.Container.profiles.Add(p);
            SetActiveProfile(field.text);
            pfManager.Container.Save(ProfilesManager.path);

            foreach (var profile in pfManager.Container.profiles)
                Debug.Log(profile.name);

            loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;

            foreach (var obj in profileButtons)
                Destroy(obj);

            profileButtons.Clear();

            if(noProfilesText.gameObject.activeSelf)
                noProfilesText.gameObject.SetActive(false);

            loadedProfiles = false;
            loginPage.gameObject.SetActive(false);
            createProfilePage.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
    }
}