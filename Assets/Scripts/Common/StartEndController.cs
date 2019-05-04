using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndController : MonoBehaviour
{

    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        GameplayTimer timer = GetComponent<GameplayTimer>();
        timer.Format = GameplayTimer.TimerFormat.s;
        timer.StartTime = 3f;
        Messenger.AddListener(GameEvents.TIMER_STOP,OnStartGameEvent);
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
}
