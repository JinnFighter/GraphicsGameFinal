using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class EnqueueCorrectAnswerAudioClipSystem : IEcsRunSystem
    {
        private AudioPlayer _audioPlayer;
        private SoundsContainer _soundsContainer;

        private EcsFilter<CorrectAnswerEvent> _correctAnswerEventFilter;

        void IEcsRunSystem.Run()
        {
            if (!_correctAnswerEventFilter.IsEmpty())
                _audioPlayer.EnqueueClip(_soundsContainer.CorrectAnswerClip);
        }
    }
}