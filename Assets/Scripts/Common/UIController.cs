using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] public SettingsPopup settingsPopup;
    [SerializeField] public GameObject tutorImage;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit"))
        {
            PlayerPrefs.SetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit", 1);
            PlayerPrefs.Save();
            //tutorImage.SetActive(true);
        }
    }
}
