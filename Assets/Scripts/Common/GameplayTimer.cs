using UnityEngine;
using UnityEngine.UI;
using System;

public class GameplayTimer : MonoBehaviour
{
    public class TimerFormat
    {
        public static string s_template_timerText = "0";
        public static string smms_templater_timerText = "00:00:000";
        public static string s = "{0:0}";
        public static string smms = "{0:00}:{1:00}:{2:000}";
    }
    private float currentTime = 0f;
    [SerializeField] public Text timerText;

    public bool Counting { get; set; }
    public float TimeLeft { get; set; }
    public float StartTime { get; set; } = 4f;
    public string Format { get; set; }

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
            if( Format == TimerFormat.s)
                timerText.text = String.Format(Format,(int)(currentTime % 60));
            else
            {
                timerText.text = String.Format(Format, (int)(currentTime / 60f) % 60, 
                    (int)(currentTime % 60), (int)(currentTime * 1000f) % 1000);
            }
            if (currentTime <= 0.0000f)
            {
                currentTime = 0.0000f;
                if (Format == TimerFormat.s)
                {
                    timerText.text = String.Format(Format, (int)(currentTime % 60));
                }
                else
                {
                    timerText.text = String.Format(Format, (int)(currentTime / 60f) % 60,
                        (int)(currentTime % 60), (int)(currentTime * 1000f) % 1000);
                }
                
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
}

