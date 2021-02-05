using UnityEngine;

public class GameOverState : GameState
{
    [SerializeField] private GameOverPresenter _endgameScreen;
    [SerializeField] private StatTrackerController _statTracker;

    public override void Init()
    {
        _endgameScreen.gameObject.SetActive(true);
        _endgameScreen.UpdateData(_statTracker.GetData());
    }

    public override void OnDelete()
    {
        _endgameScreen.gameObject.SetActive(false);
        _statTracker.ResetData();
    }
}
