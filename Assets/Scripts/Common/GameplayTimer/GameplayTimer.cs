using UnityEngine;

public class GameplayTimer : MonoBehaviour
{
    private float currentTime = 0f;
    [SerializeField] private TimerOutput output;

    public bool Counting { get; set; }
    public float TimeLeft { get; set; }
    public float StartTime { get; set; } = 4f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = StartTime;
        TimeLeft = StartTime;
        Counting = false;
       
        Messenger.AddListener(GameEvents.START_GAME,GameStarter);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.START_GAME, GameStarter);
    }

    // Update is called once per frame
    void Update()
    {
        if(Counting)
        {
            currentTime -= Time.deltaTime;
            output.DisplayTime(currentTime);

            if (currentTime <= 0.0000f)
            {
                ResetTimer();
                
                Counting = false;
                Messenger.Broadcast(GameEvents.TIMER_STOP);
            }
        }
        
    }

    public void StartTimer()
    {
        currentTime = StartTime;
        Counting = true;
    }

    public void StopTimer()
    {
        Counting = false;
        TimeLeft = currentTime;
    }

    public void PauseTimer() => Counting = false;

    public void ResumeTimer() => Counting = true;

    public void GameStarter()
    {
        var checker = GetComponent<GameField>();
        if(checker == null)
            StartTimer();
    }

    public void ResetTimer()
    {
        currentTime = 0.0000f;
        output.DisplayTime(currentTime);
    }

    public void SetStartTime(float time) => StartTime = time;

    public TimerOutput GetOutput() => output;
}

