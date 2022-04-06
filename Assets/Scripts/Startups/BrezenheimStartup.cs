using System.Collections.Generic;
using Configurations.Script;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects;
using Pixelgrid.Systems.Answers;
using Pixelgrid.Systems.Audio;
using Pixelgrid.Systems.Execution;
using Pixelgrid.Systems.GameField;
using Pixelgrid.Systems.GameModes.Brezenheim;
using Pixelgrid.Systems.Timers;
using Pixelgrid.Systems.UI.ProgressBar;
using Pixelgrid.Systems.UI.Timer;
using Pixelgrid.UI.Views;
using UnityEngine;

namespace Pixelgrid.Startups {
    sealed class BrezenheimStartup : MonoBehaviour 
    {
        [SerializeField] EcsUiEmitter _ecsUiEmitter;
        private EcsWorld _world;
        private EcsSystems _logicSystems;
        private EcsSystems _uiSystems;

        private BrezenheimModels _brezenheimModels = new BrezenheimModels();

        public GameFieldConfigs GameFieldConfigs;
        public BrezenheimConfigs BrezenheimConfigs;
        public GameModeConfiguration GameModeConfiguration;
        public DifficultyConfiguration difficultyConfiguration;
        public FlexibleGridLayout Grid;
        public TimersContainer timersContainer;
        public LinesGenerator LinesGenerator;
        public BrezenheimDataView BrezenheimDataView;
        public AudioPlayer AudioPlayer;
        public CountdownScreenPresenter CountdownPresenter;
        public EndgameScreenPresenter EndgamePresenter;
        public TutorialScreenPresenter TutorialPresenter;
        public ProgressBar ProgressBar;
        public UiScreenContainer ScreenContainer;
        public GameContent GameContent;

        void Start ()
        {
            var i18n = I18n.Instance;
            I18n.SetLocale("ru-RU");
            // void can be switched to IEnumerator for support coroutines.
            _world = new EcsWorld ();
            _logicSystems = new EcsSystems (_world);
            _uiSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_logicSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_uiSystems);
#endif

            var systemNamesContainer = new SystemNamesContainer();
            var systemNames = systemNamesContainer.Systems;
            systemNames.Add("Pausable", new List<string> 
            {
                "UpdateTimers",
                "UpdateStopwatches",
                "CheckClick"
            });

            _brezenheimModels = new BrezenheimModels();

            _logicSystems
                 // register your systems here:

                 //InitSystems go here:
                 .Add(new LoadPlayerSystem())
                 .Add(new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new GenerateCountdownTimersSystem())
                 .Add(new GenerateTimersSystem())
                 .Add(new CreateStatDataTrackerSystem())
                 .Add(new SelectMaxLineLengthSystem())
                 .Add(new LoadTutorialMessageSystem())
                 .Add(new LaunchGameplayLoopSystem())
                 //The rest of the systems go here:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())
                 .Add(new UpdateTimersSystem(), "UpdateTimers")
                 .Add(new UpdateStopwatchesSystem(), "UpdateStopwatches")
                 .Add(new GenerateLineDataSystem())
                 .Add(new GenerateDDataSystem())
                 .Add(new ResetAnswersModelSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new DrawFirstLineSystem())
                 .Add(new LaunchCountdownTimerSystem())
                 .Add(new StartGameOnCountdownTimerEndSystem())
                 .Add(new CheckClickSystem(), "CheckClick")
                 .Add(new LaunchGameplayTimerSystem())
                 .Add(new LaunchStatTrackerStopwatchSystem())
                 .Add(new CheckBrezenheimAnswerSystem())
                 .Add(new UpdateAnswersModelSystem())
                 .Add(new UpdateStatDataSystem())
                 .Add(new ClearGridSystem())
                 .Add(new UpdateGameFieldPixelsSystem())
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
                 .Inject(_brezenheimModels.BrezenheimIndexModel)
                 .Inject(_brezenheimModels.LineDataModel)
                 .Inject(_brezenheimModels.AnswersModel)
                 .Inject(GameFieldConfigs)
                 .Inject(GameModeConfiguration)
                 .Inject(BrezenheimConfigs)
                 .Inject(difficultyConfiguration)
                 .Inject(Grid)
                 .Inject(timersContainer)
                 .Inject(LinesGenerator)
                 .Inject(BrezenheimDataView)
                 .Inject(GameContent.SpritesContent.PixelSpritesContent)
                 .Inject(GameContent.PrefabsContent)
                 .Inject(GameContent.AudioContent)
                 .Inject(AudioPlayer)
                 .Inject(CountdownPresenter)
                 .Inject(EndgamePresenter)
                 .Inject(TutorialPresenter)
                 .Inject(ScreenContainer)
                 .Inject(i18n)
                 .Init ();
            
            _uiSystems
                // Init systems go here:    
                .Add(new InitCountdownTimerViewSystem())
                .Add(new InitGameplayTimerViewSystem())
                .Add(new InitProgressBarViewSystem())
                // RunSystems go here:
                .Add(new UpdateTimerViewSystem())
                .Add(new UpdateProgressBarViewSystem())
                // DestroySystems go here:
                .Add(new DestroyTimerViewsSystem())
                .Add(new DestroyProgressBarViewSystem())
                // Inject services here:
                .Inject(timersContainer)
                .Inject(_brezenheimModels.AnswersModel)
                .Inject(ProgressBar)
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
            
            if (_logicSystems != null) {
                _logicSystems.Destroy ();
                _logicSystems = null;
            }
            
            _world.Destroy();
            _world = null;
        }
    }
}