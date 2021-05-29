using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class LoadTutorialMessageSystem : IEcsInitSystem 
    {
        private TutorialScreenPresenter _tutorialScreenPresenter;
        private I18n _i18n;

        public void Init()
        {
            _tutorialScreenPresenter.SetText(_i18n.__("Hello"));
        }
    }
}