using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    public float CurrentTime { get; private set; }
    public bool IsCounting { get; private set; }
    public float StartTime { get; set; }

    public delegate void TimeTick(float currentTime);

    public event TimeTick TimeChange;

    public delegate void TimerEnd();

    public event TimerEnd TimerEndEvent;

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
                ResetTimer();

            TimeChange?.Invoke(CurrentTime);
            if (CurrentTime <= 0.0000f)
            {
                IsCounting = false;
                TimerEndEvent?.Invoke();
                Messenger.Broadcast(GameEvents.TIMER_STOP);
            }
        }
    }

    public void StopTimer() => IsCounting = false;
    public void ResumeTimer() => IsCounting = true;
    public void ResetTimer() => CurrentTime = 0.0000f;

    public void Launch()
    {
        CurrentTime = StartTime;
        IsCounting = true;
    }
}
