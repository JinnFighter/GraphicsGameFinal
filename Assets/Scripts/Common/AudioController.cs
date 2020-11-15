using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : Controller
{
    [SerializeField] private AudioClip _correctAnswerClip;
    [SerializeField] private AudioClip _wrongAnswerClip;
    [SerializeField] private AudioClip _gameOverClip;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();

        actions.Add(GameEvents.ACTION_RIGHT_ANSWER, PlayCorrectAnswerClip);
        actions.Add(GameEvents.ACTION_WRONG_ANSWER, PlayWrongAnswerClip);
        actions.Add(GameEvents.GAME_OVER, PlayGameOverClip);
    }

    private void PlayCorrectAnswerClip() => PlayClip(_correctAnswerClip);

    private void PlayWrongAnswerClip() => PlayClip(_wrongAnswerClip);

    private void PlayGameOverClip() => PlayClip(_gameOverClip);

    public void PlayClip(AudioClip audioClip)
    {
        if (_source.clip != audioClip)
            _source.clip = audioClip;

        _source.Play();
    }
}
