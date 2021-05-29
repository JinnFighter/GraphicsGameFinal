using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _source;

        private Queue<AudioClip> _clipQueue;

        void Awake()
        {
            _source = GetComponent<AudioSource>();
            _clipQueue = new Queue<AudioClip>();
        }

        public void EnqueueClip(AudioClip audioClip) => _clipQueue.Enqueue(audioClip);
        
        public void PlayClips()
        {
            while (_clipQueue.Any())
                _source.PlayOneShot(_clipQueue.Dequeue());
        }
    }
}
