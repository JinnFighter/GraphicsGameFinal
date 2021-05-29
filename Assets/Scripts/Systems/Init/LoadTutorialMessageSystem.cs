using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class LoadTutorialMessageSystem : IEcsInitSystem 
    {
        private TutorialScreenPresenter _tutorialScreenPresenter;
        private I18n _i18n;
        private GameModeConfiguration _gameModeConfiguration;

        public void Init()
        {
            _tutorialScreenPresenter.SetText(_i18n.__($"Tutorial{_gameModeConfiguration.Name}"));
        }
    }
}