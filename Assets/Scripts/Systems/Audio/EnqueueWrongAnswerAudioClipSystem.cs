using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class EnqueueWrongAnswerAudioClipSystem : IEcsRunSystem
    {
        private AudioPlayer _audioPlayer;
        private SoundsContainer _soundsContainer;

        private EcsFilter<WrongAnswerEvent> _wrongAnswerEventFilter;

        void IEcsRunSystem.Run()
        {
            if (!_wrongAnswerEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_soundsContainer.WrongAnswerClip);
        }
    }
}