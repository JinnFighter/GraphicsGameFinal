using System.Collections.Generic;
using System.Linq;
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
        pfManager = GetComponent<ProfilesManager>();
        
        if(pfManager.Container.profiles.Count == 0)
        {
            var profile = new PlayerProfile { active = true, name = "Jinn" };
            pfManager.Container.profiles.Add(profile);
            pfManager.ActiveProfile = profile;
            pfManager.Container.Save(ProfilesManager.path);
        }
        if(pfManager.ActiveProfile != null)
            loginButton.GetComponentInChildren<Text>().text = pfManager.ActiveProfile.name;
    }

    public void LoadProfiles(GameObject panel)
    {
        if(loadedProfiles) return;

        if (pfManager.Container.profiles.Count != 0)
        {
            foreach(var profile in pfManager.Container.profiles)
            {
                var btn = Instantiate(template) as GameObject;
                btn.GetComponentInChildren<Text>().text = profile.name;
                btn.transform.SetParent(panel.transform);
                var buttonComponent = btn.GetComponent<Button>();
                buttonComponent.onClick = new Button.ButtonClickedEvent();
                buttonComponent.onClick.AddListener(
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
        foreach (var profile in pfManager.Container.profiles.Where(profile => profile.active))
                profile.active = false;

        foreach (var profile in pfManager.Container.profiles.Where(profile => profile.name == profileName))
        {
                profile.active = true;
                pfManager.ActiveProfile = profile;
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