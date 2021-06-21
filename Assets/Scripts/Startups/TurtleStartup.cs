using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
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
        public GameState GameState;
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
            _systems
                 // register your systems here, for example:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())

                 //InitSystems go here:
                 .Add(new LoadPlayerSystem())
                 .Add(new CreateGameplayEventReceiverSystem())
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
                 .Add(new UpdateTimersSystem())
                 .Add(new UpdateStopwatchesSystem())
                 .Add(new GenerateTurtlePathSystem())
                 .Add(new ResetTurtleProgressBarSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new ResetTurtlePositionSystem())
                 .Add(new LaunchCountdownTimerSystem())
                 .Add(new StartGameOnCountdownTimerEndSystem())
                 .Add(new CheckTurtleClickSystem())
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
                 .Add(new SendStatDataToServerSystem())
                 .Add(new EnqueueCorrectAnswerAudioClipSystem())
                 .Add(new EnqueueWrongAnswerAudioClipSystem())
                 .Add(new EnqueueGameOverAudioClipSystem())
                 .Add(new PlayAudioClipSystem())
                 .InjectUi(_ecsUiEmitter)

                 // register one-frame components (order is important), for example:
                 .OneFrame<TurtleCommand>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<TimerEndEvent>()
                 .OneFrame<CorrectAnswerEvent>()
                 .OneFrame<WrongAnswerEvent>()
                 .OneFrame<GameOverEvent>()
                 .OneFrame<RestartGameEvent>()
                 .OneFrame<LineDrawData>()
                 .OneFrame<ClearGridEvent>()

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
                .Inject(GameState)
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
