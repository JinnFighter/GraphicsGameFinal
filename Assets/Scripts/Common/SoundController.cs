using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]private AudioClip correctAnswerClip;
    [SerializeField]private AudioClip wrongAnswerClip;
    [SerializeField]private AudioClip gameOverClip;
    [SerializeField]private AudioClip buttonClickClip;
    [SerializeField]private AudioClip selectProfileClip;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();;
    }

    public void PlayCorrectAnswerClip(int a) => PlayClip(correctAnswerClip);

    public void PlayWrongAnswerClip() => PlayClip(wrongAnswerClip);

    public void PlayGameOverClip() => PlayClip(gameOverClip);

    public void PlayButtonClickClip() => PlayClip(buttonClickClip);

    public void PlaySelectProfileClip() => PlayClip(selectProfileClip);

    public void PlayClip(AudioClip audioClip) => _source.PlayOneShot(audioClip);
}