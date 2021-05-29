using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class EnqueueGameOverAudioClipSystem : IEcsRunSystem 
    {
        private AudioPlayer _audioPlayer;
        private SoundsContainer _soundsContainer;

        private EcsFilter<GameOverEvent> _gameOverEventFilter;

        void IEcsRunSystem.Run()
        {
            if (!_gameOverEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_soundsContainer.GameOverClip);
        }
    }
}