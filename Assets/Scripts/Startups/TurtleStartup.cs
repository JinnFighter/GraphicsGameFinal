using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using System.Collections.Generic;
using Pixelgrid.Systems.Execution;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    sealed class TurtleStartup : MonoBehaviour
    {
        [SerializeField] EcsUiEmitter _ecsUiEmitter;
        EcsWorld _world;
        EcsSystems _systems;

        public GameModeConfiguration GameModeConfiguration;
        public DifficultyConfiguration difficultyConfiguration;
        public GameFieldConfiguration gameFieldConfiguration;
        public SpritesContainer spritesContainer;
        public TurtleSpritesContainer TurtleSpritesContainer;
        public TimersContainer timersContainer;
        public TurtleConfiguration TurtleConfiguration;
        public SoundsContainer SoundsContainer;
        public AudioPlayer AudioPlayer;
        public CountdownScreenPresenter CountdownPresenter;
        public EndgameScreenPresenter EndgamePresenter;
        public TutorialScreenPresenter TutorialPresenter;
        public ProgressBar ProgressBar;
        public UiScreenContainer ScreenContainer;
        public Text PathText;

        void Start()
        {
            var i18n = I18n.Instance;
            I18n.SetLocale("ru-RU");
            // void can be switched to IEnumerator for support coroutines.
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            
            var systemNamesContainer = new SystemNamesContainer();
            var systemNames = systemNamesContainer.Systems;
            systemNames.Add("Pausable", new List<string> 
            {
                "UpdateTimers",
                "UpdateStopwatches",
                "CheckTurtleClick"
            });
            
            _systems
                 // register your systems here:

                 //InitSystems go here:
                 .Add(new LoadPlayerSystem())
                 .Add(new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new GenerateCountdownTimersSystem())
                 .Add(new GenerateTimersSystem())
                 .Add(new CreateStatDataTrackerSystem())
                 .Add(new CreateGameModeDataContainerSystem())
                 .Add(new CreateProgressBarSystem())
                 .Add(new LoadTutorialMessageSystem())
                 .Add(new CreateTurtleSystem())
                 .Add(new CreateTurtlePathSystem())
                 .Add(new LaunchGameplayLoopSystem())
                 //The rest of the systems go here:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())
                 .Add(new UpdateTimersSystem(), "UpdateTimers")
                 .Add(new UpdateStopwatchesSystem(), "UpdateStopwatches")
                 .Add(new GenerateTurtlePathSystem())
                 .Add(new ResetTurtleProgressBarSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new ResetTurtlePositionSystem())
                 .Add(new LaunchCountdownTimerSystem())
                 .Add(new StartGameOnCountdownTimerEndSystem())
                 .Add(new CheckTurtleClickSystem(), "CheckTurtleClick")
                 .Add(new LaunchGameplayTimerSystem())
                 .Add(new LaunchStatTrackerStopwatchSystem())
                 .Add(new CheckTurtleAnswerSystem())
                 .Add(new UpdateTurtleSpritesSystem())
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
                 .Add(new DisableSystemsByTypeSystem(_systems, systemNamesContainer))
                 .Add(new EnableSystemsByTypeSystem(_systems, systemNamesContainer))
                 .Add(new UpdateTimerTextSystem())
                 .Add(new UpdateUiTextSystem())
                 .Add(new EnqueueCorrectAnswerAudioClipSystem())
                 .Add(new EnqueueWrongAnswerAudioClipSystem())
                 .Add(new EnqueueGameOverAudioClipSystem())
                 .Add(new PlayAudioClipSystem())
                 .InjectUi(_ecsUiEmitter)

                 // register one-frame components (order is important), for example:
                 .OneFrame<TurtleCommand>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<TimeChangeEvent>()
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
                 .OneFrame<UpdateTextEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(GameModeConfiguration)
                .Inject(difficultyConfiguration)
                .Inject(gameFieldConfiguration)
                .Inject(TurtleConfiguration)
                .Inject(spritesContainer)
                .Inject(TurtleSpritesContainer)
                .Inject(timersContainer)
                .Inject(SoundsContainer)
                .Inject(AudioPlayer)
                .Inject(CountdownPresenter)
                .Inject(EndgamePresenter)
                .Inject(TutorialPresenter)
                .Inject(ProgressBar)
                .Inject(ScreenContainer)
                .Inject(PathText)
                .Inject(i18n)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}
