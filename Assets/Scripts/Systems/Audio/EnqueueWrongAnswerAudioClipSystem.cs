using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Audio;

namespace Pixelgrid.Systems.Audio
{
    public sealed class EnqueueWrongAnswerAudioClipSystem : IEcsRunSystem
    {
        private readonly AudioPlayer _audioPlayer = null;
        private readonly AudioContent _audioContent = null;

        private readonly EcsFilter<WrongAnswerEvent> _wrongAnswerEventFilter = null;

        void IEcsRunSystem.Run()
        {
            if (!_wrongAnswerEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_audioContent.WrongAnswerClip);
        }
    }
}