using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour, IPausable
{
    [SerializeField] private GameObject _tutorialScreen;
    [SerializeField] private UiScreenContainer _uiScreenContainer;
    [SerializeField] private StatesContainer _statesContainer;
    [SerializeField] List<Controller> _controllers;
    [SerializeField] private ProfilesManager _profilesManager;

    // Start is called before the first frame update
    void Start()
    {
        _statesContainer.OnStartEvent();
        if(IsFirstTimePlaying())
        {
            _statesContainer.Pause();
            _uiScreenContainer.Push(_tutorialScreen);
        }
    }


    public void Push(GameObject uiScreen)
    {
        if (_uiScreenContainer.GetCount() == 0)
            _statesContainer.Pause();

        _uiScreenContainer.Push(uiScreen);
    }

    public void Pop()
    {
        _uiScreenContainer.Pop();
        if (_uiScreenContainer.GetCount() == 0)
            _statesContainer.Unpause();
    }

    public void Pause()
    {
        _statesContainer.Pause();
        Notify(GameEvents.PAUSE_GAME);
    }

    public void Unpause()
    {
        _statesContainer.Unpause();
        Notify(GameEvents.CONTINUE_GAME);
    }

    private void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }

    private bool IsFirstTimePlaying()
    {
        var prefId = _profilesManager.ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit";
        var visitState = PlayerPrefs.HasKey(prefId) ? PlayerPrefs.GetInt(prefId) : 1;
        if (visitState == 1)
        {
            PlayerPrefs.SetInt(prefId, 0);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}
