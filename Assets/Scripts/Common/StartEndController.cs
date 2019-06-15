using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndController : MonoBehaviour
{

    bool start = true;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private Text originalText;
    private List<Text> texts;
    // Start is called before the first frame update
    void Start()
    {
        texts = new List<Text>();
        GameplayTimer timer = GetComponent<GameplayTimer>();
        timer.Format = GameplayTimer.TimerFormat.s;
        timer.StartTime = 4f;
        Messenger.AddListener(GameEvents.TIMER_STOP,OnStartGameEvent);
        Messenger.AddListener(GameEvents.GAME_OVER, OnEndGameEvent);
        //timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.TIMER_STOP, OnStartGameEvent);
        Messenger.RemoveListener(GameEvents.GAME_OVER, OnEndGameEvent);
    }
    public void OnStartGameEvent()
    {
        if(start)
        {
            start = false;
            GetComponent<GameplayTimer>().timerText.gameObject.SetActive(false);
        }
        else
        {
            Messenger.Broadcast(GameEvents.GAME_OVER);
        }
        
    }
    public void OnEndGameEvent()
    {
        
        string playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
        int score = GetComponent<ScoreKeeper>().Score;
        GetComponent<Leaderboard>().AddScore(playerName, score);
        
        originalText.text = GetComponent<Leaderboard>().Container.boardMembers[0].name
            + GetComponent<Leaderboard>().Container.boardMembers[0].score;
        if(texts.Count!=0)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].transform.SetParent(null);
                Destroy(texts[i]);
            }
            texts.Clear();
        }
        
        for(int i=1;i< GetComponent<Leaderboard>().Container.boardMembers.Count;i++)
        {
            playerName = GetComponent<Leaderboard>().Container.boardMembers[i].name;
            score = GetComponent<Leaderboard>().Container.boardMembers[i].score;
            Text text = Instantiate(originalText) as Text;
            text.text = playerName + score;
            text.transform.SetParent(originalText.transform.parent);
            texts.Add(text);
        }
        endgameScreen.SetActive(true);


    }
    public void OnRestartButtonClicked()
    {
        start = true;
        Messenger.Broadcast(GameEvents.RESTART_GAME);
    }
    public void OnQuitButtonClicked()
    {
        Messenger<string>.Broadcast(GameEvents.QUIT_GAME, "MainMenu");
    }
}

