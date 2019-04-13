using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameplayTimer : MonoBehaviour
{
    private float startTime=10f;
    private float currentTime=0f;
    private bool counting;
    private float timeLeft;
    [SerializeField] public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        timeLeft = startTime;
        counting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(counting)
        {
            currentTime -= Time.deltaTime;
            timerText.text = String.Format("{0:00}:{1:00}:{2:000}", (int)(currentTime / 60f) % 60, (int)(currentTime % 60), (int)(currentTime * 1000f) % 1000);
            //timerText.text = currentTime.ToString("0");
            if (currentTime <= 0.0000f)
            {
                currentTime = 0.0000f;
                timerText.text = String.Format("{0:00}:{1:00}:{2:000}", (int)(currentTime / 60f) % 60, (int)(currentTime % 60), (int)(0 * 1000f) % 1000);
                counting = false;
            }
        }
        
    }
    public void StartTimer()
    {
        currentTime = startTime;
        counting = true;
    }
    public void StopTimer()
    {
        counting = false;
        timeLeft = currentTime;
    }
}
