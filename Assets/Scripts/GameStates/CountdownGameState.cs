using UnityEngine;

public class CountdownGameState : MonoBehaviour, IGameState
{
    private TimerComponent _timer;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
        _timer.StartTime = 4f;
    }

    public void Init()
    {
        _timer.Launch();
        _timer.GetOutput().gameObject.SetActive(true);
    }

    public void OnDelete()
    {
        _timer.StopTimer();
        _timer.GetOutput().gameObject.SetActive(false);
    }
}
