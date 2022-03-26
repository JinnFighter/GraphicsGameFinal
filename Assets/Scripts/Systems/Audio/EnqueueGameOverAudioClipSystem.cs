using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Audio;

namespace Pixelgrid.Systems.Audio 
{
    public sealed class EnqueueGameOverAudioClipSystem : IEcsRunSystem 
    {
        private readonly AudioPlayer _audioPlayer = null;
        private readonly AudioContent _audioContent = null;

        private readonly EcsFilter<GameOverEvent> _gameOverEventFilter = null;

        void IEcsRunSystem.Run()
        {
            if (!_gameOverEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_audioContent.GameOverClip);
        }
    }
}