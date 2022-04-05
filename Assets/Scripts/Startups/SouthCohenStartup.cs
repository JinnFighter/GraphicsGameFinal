using System.Collections.Generic;
using Configurations.Script;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects;
using Pixelgrid.Systems;
using Pixelgrid.Systems.Audio;
using Pixelgrid.Systems.Execution;
using Pixelgrid.Systems.GameField;
using Pixelgrid.Systems.GameModes.SouthCohen;
using Pixelgrid.Systems.Timers;
using Pixelgrid.Systems.UI.Timer;
using UnityEngine;

namespace Pixelgrid.Startups
{
    sealed class SouthCohenStartup : MonoBehaviour
    {
        [SerializeField] EcsUiEmitter _ecsUiEmitter;
        private EcsWorld _world;
        private EcsSystems _logicSystems;
        private EcsSystems _uiSystems;

        private SouthCohenModels _southCohenModels;

        public SouthCohenConfigs SouthCohenConfigs;
        public GameFieldConfigs GameFieldConfigs;
        public GameModeConfiguration GameModeConfiguration;
        public DifficultyConfiguration difficultyConfiguration;
        public FlexibleGridLayout Grid;
        public TimersContainer timersContainer;
        public SouthCohenLinesGenerator LinesGenerator;
        public AudioPlayer AudioPlayer;
        public CountdownScreenPresenter CountdownPresenter;
        public EndgameScreenPresenter EndgamePresenter;
        public TutorialScreenPresenter TutorialPresenter;
        public ProgressBar ProgressBar;
        public UiScreenContainer ScreenContainer;
        public CodeReceiver CodeReceiver;
        public GameObject Border;
        public GameContent GameContent;

        void Start()
        {
            var i18n = I18n.Instance;
            I18n.SetLocale("ru-RU");
            // void can be switched to IEnumerator for support coroutines.
            _world = new EcsWorld();
            _logicSystems = new EcsSystems(_world);
            _uiSystems = new EcsSystems(_world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_logicSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_uiSystems);
#endif

            var systemNamesContainer = new SystemNamesContainer();
            var systemNames = systemNamesContainer.Systems;
            systemNames.Add("Pausable", new List<string> 
            {
                "UpdateTimers",
                "UpdateStopwatches",
                "CheckClick"
            });

            _southCohenModels = new SouthCohenModels();

            _logicSystems
                 // register your systems here:

                 //InitSystems go here:
                 .Add(new LoadPlayerSystem())
                 .Add(new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new GenerateCountdownTimersSystem())
                 .Add(new GenerateTimersSystem())
                 .Add(new CreateStatDataTrackerSystem())
                 .Add(new GenerateBorderSystem())
                 .Add(new LoadTutorialMessageSystem())
                 .Add(new LaunchGameplayLoopSystem())
                 //The rest of the systems go here:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())
                 .Add(new UpdateTimersSystem(), "UpdateTimers")
                 .Add(new UpdateStopwatchesSystem(), "UpdateStopwatches")
                 .Add(new GenerateSouthCohenLinesSystem())
                 .Add(new GenerateLineZonesSystem())
                 .Add(new ResetProgressBarSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new DrawFirstSouthCohenLineSystem())
                 .Add(new LaunchCountdownTimerSystem())
                 .Add(new StartGameOnCountdownTimerEndSystem())
                 .Add(new CheckClickSystem(), "CheckClick")
                 .Add(new LaunchGameplayTimerSystem())
                 .Add(new LaunchStatTrackerStopwatchSystem())
                 .Add(new CheckSouthCohenAnswerSystem())
                 .Add(new UpdateAnswersModelSystem())
                 .Add(new UpdateStatDataSystem())
                 .Add(new ClearGridSystem())
                 .Add(new UpdateGameFieldPixelsSystem())
                 .Add(new UpdateProgressBarSystem())
                 .Add(new GameOverOnTimerEndSystem())
                 .Add(new DisableStopwatchOnGameOverSystem())
                 .Add(new DisableGameplayTimerOnGameOverSystem())
                 .Add(new ShowEndgameScreenSystem())
                 .Add(new PauseSystem())
                 .Add(new UnpauseSystem())
                 .Add(new DisableSystemsByTypeSystem(_logicSystems, systemNamesContainer))
                 .Add(new EnableSystemsByTypeSystem(_logicSystems, systemNamesContainer))
                 .Add(new UpdateImageSpritesSystem())
                 .Add(new EnqueueCorrectAnswerAudioClipSystem())
                 .Add(new EnqueueWrongAnswerAudioClipSystem())
                 .Add(new EnqueueGameOverAudioClipSystem())
                 .Add(new PlayAudioClipSystem())
                 .InjectUi(_ecsUiEmitter)

                 // register one-frame components (order is important), for example:
                 .OneFrame<PixelClickedEvent>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<GameModeDataGeneratedEvent>()
                 .OneFrame<TimerEndEvent>()
                 .OneFrame<CorrectAnswerEvent>()
                 .OneFrame<WrongAnswerEvent>()
                 .OneFrame<GameOverEvent>()
                 .OneFrame<RestartGameEvent>()
                 .OneFrame<LineDrawData>()
                 .OneFrame<ClearGridEvent>()
                 .OneFrame<PauseEvent>()
                 .OneFrame<UnpauseEvent>()
                 .OneFrame<DisableSystemTypeEvent>()
                 .OneFrame<EnableSystemTypeEvent>()
                 .OneFrame<UpdateSpriteImageEvent>()

                 // inject service instances here (order doesn't important), for example:
                 .Inject(_southCohenModels.LineDataModel)
                 .Inject(_southCohenModels.SouthCohenDataModel)
                 .Inject(_southCohenModels.AnswersModel)
                 .Inject(SouthCohenConfigs)
                 .Inject(GameFieldConfigs)
                 .Inject(GameModeConfiguration)
                 .Inject(difficultyConfiguration)
                 .Inject(Grid)
                 .Inject(timersContainer)
                 .Inject(LinesGenerator)
                 .Inject(CodeReceiver)
                 .Inject(Border)
                 .Inject(GameContent.SpritesContent.PixelSpritesContent)
                 .Inject(GameContent.PrefabsContent)
                 .Inject(GameContent.AudioContent)
                 .Inject(AudioPlayer)
                 .Inject(CountdownPresenter)
                 .Inject(EndgamePresenter)
                 .Inject(TutorialPresenter)
                 .Inject(ProgressBar)
                 .Inject(ScreenContainer)
                 .Inject(i18n)
                 .Init();
            
            _uiSystems
                // Init systems go here:    
                .Add(new InitCountdownTimerViewSystem())
                .Add(new InitGameplayTimerViewSystem())
                // RunSystems go here:
                .Add(new UpdateTimerViewSystem())
                // DestroySystems go here:
                .Add(new DestroyTimerViewsSystem())
                // Inject services here:
                .Inject(timersContainer)
                .Init();
        }

        void Update()
        {
            _logicSystems?.Run();
            _uiSystems?.Run();
        }

        void OnDestroy()
        {
            if (_uiSystems != null)
            {
                _uiSystems.Destroy();
                _uiSystems = null;
            }
            
            if (_logicSystems != null)
            {
                _logicSystems.Destroy();
                _logicSystems = null;
            }
            
            _world.Destroy();
            _world = null;
        }
    }
}
