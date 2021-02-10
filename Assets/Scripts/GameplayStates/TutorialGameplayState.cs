using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameplayState : GameplayState
{
    [SerializeField] private GameObject _tutorialImage;
    [SerializeField] private StatesContainer _statesContainer;
    [SerializeField] private ProfilesManager _profilesManager;

    public override void Init()
    {
        if (IsFirstTimePlaying())
            _tutorialImage.SetActive(true);
        else
            _statesContainer.NextState();
    }

    public override void OnDelete() => _tutorialImage.SetActive(false);

    protected override void OnPauseAction()
    {
        
    }

    protected override void OnUnpauseAction()
    {
        
    }

    private bool IsFirstTimePlaying()
    {
        var prefId = _profilesManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit";
        var visitState =  PlayerPrefs.HasKey(prefId) ? PlayerPrefs.GetInt(prefId) : 1;
        if(visitState == 1)
        {
            PlayerPrefs.SetInt(prefId, 0);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}
