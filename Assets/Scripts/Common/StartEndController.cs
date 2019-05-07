using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEndController : MonoBehaviour
{

    bool start = true;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private Text originalText;
    // Start is called before the first frame update
    void Start()
    {
        GameplayTimer timer = GetComponent<GameplayTimer>();
        timer.Format = GameplayTimer.TimerFormat.s;
        timer.StartTime = 3f;
        Messenger.AddListener(GameEvents.TIMER_STOP,OnStartGameEvent);
        Messenger.AddListener(GameEvents.GAME_OVER, OnEndGameEvent);
        //timer.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        }
        
    }
    public void OnEndGameEvent()
    {
        string playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
        int score = GetComponent<ScoreKeeper>().Score;
        GetComponent<Leaderboard>().AddScore(playerName, score);
        
        originalText.text = GetComponent<Leaderboard>().Container.boardMembers[0].name
            + GetComponent<Leaderboard>().Container.boardMembers[0].score;
        for(int i=1;i< GetComponent<Leaderboard>().Container.boardMembers.Count;i++)
        {
            playerName = GetComponent<Leaderboard>().Container.boardMembers[i].name;
            score = GetComponent<Leaderboard>().Container.boardMembers[i].score;
            Text text = Instantiate(originalText) as Text;
            text.text = playerName + score;
            text.transform.SetParent(originalText.transform.parent);
        }
        endgameScreen.SetActive(true);
        //GetComponent<Leaderboard>().AddScore(GetComponent<ProfilesManager>().ActiveProfile.name,
        //  GetComponent<ScoreKeeper>().Score);


    }
    public void OnRestartButtonClicked()
    {
        Messenger.Broadcast(GameEvents.RESTART_GAME);
    }
    public void OnQuitButtonClicked()
    {
        Messenger<string>.Broadcast(GameEvents.QUIT_GAME, "MainMenu");
    }
}
