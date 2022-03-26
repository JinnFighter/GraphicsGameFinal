using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Audio
{
    [CreateAssetMenu(fileName = "AudioContent", menuName = "Content/AudioContent")]
    public class AudioContent : ScriptableObject
    {
        [field: SerializeField] public AudioClip GameOverClip { get; private set; }
        [field: SerializeField] public AudioClip CorrectAnswerClip { get; private set; }
        [field: SerializeField] public AudioClip WrongAnswerClip { get; private set; }
    }
}
