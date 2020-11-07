using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    public float CurrentTime { get; private set; }
    public bool IsCounting { get; private set; }
    public float StartTime { get; set; }

    void Awake()
    {
        StartTime = 4f;
        CurrentTime = StartTime;
        IsCounting = false;
    }

    void Update()
    {
        if (IsCounting)
        {
            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0.0000f)
            {
                ResetTimer();

                IsCounting = false;
                Messenger.Broadcast(GameEvents.TIMER_STOP);
            }
        }
    }

    public void PauseTimer() => IsCounting = false;
    public void ResumeTimer() => IsCounting = true;
    public void ResetTimer() => CurrentTime = 0.0000f;
    public void StartTimer()
    {
        CurrentTime = StartTime;
        IsCounting = true;
    }
}
