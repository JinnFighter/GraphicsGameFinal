using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]private AudioSource source;
    [SerializeField]private AudioClip correctAnswerClip;
    [SerializeField]private AudioClip wrongAnswerClip;
    [SerializeField]private AudioClip gameOverClip;
    [SerializeField]private AudioClip buttonClickClip;
    [SerializeField]private AudioClip selectProfileClip;
    // Start is called before the first frame update
    void Start()
    {
        Messenger<int>.AddListener(GameEvents.ACTION_RIGHT_ANSWER, PlayCorrectAnswerClip);
        Messenger.AddListener(GameEvents.ACTION_WRONG_ANSWER, PlayWrongAnswerClip);
        Messenger.AddListener(GameEvents.GAME_OVER, PlayGameOverClip);
    }
    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvents.ACTION_RIGHT_ANSWER, PlayCorrectAnswerClip);
        Messenger.RemoveListener(GameEvents.ACTION_WRONG_ANSWER, PlayWrongAnswerClip);
        Messenger.RemoveListener(GameEvents.GAME_OVER, PlayGameOverClip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCorrectAnswerClip(int a)
    {
        if(source.clip!=correctAnswerClip)
        {
            source.clip = correctAnswerClip;
        }
        source.Play();
    }
    public void PlayWrongAnswerClip()
    {
        if(source.clip!=wrongAnswerClip)
        {
            source.clip = wrongAnswerClip;
        }
        source.Play();
    }
    public void PlayGameOverClip()
    {
        if (source.clip != gameOverClip)
        {
            source.clip = gameOverClip;
        }
        source.Play();
    }
    public void PlayButtonClickClip()
    {
        if (source.clip != buttonClickClip)
        {
            source.clip = buttonClickClip;
        }
        source.Play();
    }
    public void PlaySelectProfileClip()
    {
        if (source.clip != selectProfileClip)
        {
            source.clip = selectProfileClip;
        }
        source.Play();
    }

}
