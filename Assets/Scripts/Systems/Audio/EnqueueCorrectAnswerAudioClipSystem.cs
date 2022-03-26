using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Audio;

namespace Pixelgrid.Systems.Audio
{
    public sealed class EnqueueCorrectAnswerAudioClipSystem : IEcsRunSystem
    {
        private readonly AudioPlayer _audioPlayer = null;
        private readonly AudioContent _audioContent = null;

        private readonly EcsFilter<CorrectAnswerEvent> _correctAnswerEventFilter = null;

        void IEcsRunSystem.Run()
        {
            if (!_correctAnswerEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_audioContent.CorrectAnswerClip);
        }
    }
}