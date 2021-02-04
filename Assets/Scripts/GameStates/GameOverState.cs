using UnityEngine;

public class GameOverState : GameState
{
    [SerializeField] private GameObject _endgameScreen;
    [SerializeField] private StatTrackerController _statTracker;

    public override void Init()
    {
        _endgameScreen.SetActive(true);
    }

    public override void OnDelete()
    {
        _endgameScreen.SetActive(false);
        _statTracker.ResetData();
    }
}
