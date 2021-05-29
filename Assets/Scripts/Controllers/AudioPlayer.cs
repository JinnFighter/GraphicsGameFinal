using UnityEngine;

namespace Pixelgrid
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _source;

        void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlayClip(AudioClip audioClip) => _source.PlayOneShot(audioClip);
    }
}
