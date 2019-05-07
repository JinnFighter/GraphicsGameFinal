using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    private int streak = 0;
    [SerializeField] private Text scoreNumber;

    public int Score { get => score; set => score = value; }

    // Start is called before the first frame update
    void Start()
    {
        Messenger<int>.AddListener(GameEvents.ACTION_RIGHT_ANSWER, AddScore);
        Messenger.AddListener(GameEvents.ACTION_WRONG_ANSWER, ResetStreak);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int points)
    {
        if(streak<5)
            streak++;
        Score +=(int)(points*(1+0.1*streak));
        scoreNumber.text = Score.ToString();
    }
    public void ResetStreak()
    {
        streak = 0;
    }
    
}
