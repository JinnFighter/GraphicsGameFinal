using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class PlayAudioClipSystem : IEcsRunSystem 
    {
        private AudioPlayer _audioPlayer;
        
        void IEcsRunSystem.Run()
        {
            _audioPlayer.PlayClips();
        }
    }
}