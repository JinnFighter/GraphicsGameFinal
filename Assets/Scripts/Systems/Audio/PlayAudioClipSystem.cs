using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class PlayAudioClipSystem : IEcsRunSystem 
    {
        private AudioPlayer _audioPlayer;
        private EcsFilter<AudioClipComponent> _audioClipsFilter;
        
        void IEcsRunSystem.Run()
        {
            foreach (var index in _audioClipsFilter)
                _audioPlayer.PlayClip(_audioClipsFilter.Get1(index).AudioClip);
        }
    }
}