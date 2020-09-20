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

    public void PlayCorrectAnswerClip(int a) => PlayClip(correctAnswerClip);

    public void PlayWrongAnswerClip() => PlayClip(wrongAnswerClip);

    public void PlayGameOverClip() => PlayClip(gameOverClip);

    public void PlayButtonClickClip() => PlayClip(buttonClickClip);

    public void PlaySelectProfileClip() => PlayClip(selectProfileClip);

    public void PlayClip(AudioClip audioClip)
    {
        if (source.clip != audioClip)
            source.clip = audioClip;

        source.Play();
    }
}