using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace Pixelgrid {
    sealed class BrezenheimStartup : MonoBehaviour {

        [SerializeField] EcsUiEmitter _ecsUiEmitter;
        EcsWorld _world;
        EcsSystems _systems;

        public DifficultyConfiguration difficultyConfiguration;
        public GameFieldConfiguration gameFieldConfiguration;
        public SpritesContainer spritesContainer;
        public TimersContainer timersContainer;
        public LinesGenerator LinesGenerator;
        public BrezenheimDataContainer BrezenheimDataContainer;
        public GameState GameState;
        public EndgameScreenPresenter EndgamePresenter;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())

                 //InitSystems go here:
                 .Add(new CreateGameplayEventReceiverSystem())
                 .Add(new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new GenerateCountdownTimersSystem())
                 .Add(new GenerateTimersSystem())
                 .Add(new CreateStatDataTrackerSystem())
                 .Add(new CreateGameModeDataContainerSystem())
                 .Add(new SelectMaxLineLengthSystem())
                 .Add(new GenerateLineDataSystem())
                 .Add(new GenerateDDataSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new StartGameSystem())
                 //The rest of the systems go here:
                 .Add(new CheckClickSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new DrawFirstLineSystem())
                 .Add(new LaunchGameplayTimerSystem())
                 .Add(new LaunchStatTrackerStopwatchSystem())
                 .Add(new UpdateTimersSystem())
                 .Add(new UpdateStopwatchesSystem())
                 .Add(new CheckBrezenheimAnswerSystem())
                 .Add(new UpdateDDataSystem())
                 .Add(new UpdateStatDataSystem())
                 .Add(new ClearGridSystem())
                 .Add(new UpdateGameFieldPixelsSystem())
                 .Add(new GameOverOnTimerEndSystem())
                 .Add(new DisableStopwatchOnGameOverSystem())
                 .Add(new DisableGameplayTimerOnGameOverSystem())
                 .Add(new ShowEndgameScreenSystem())
                 .InjectUi(_ecsUiEmitter)

                // register one-frame components (order is important), for example:
                 .OneFrame<PixelClickedEvent>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<TimerEndEvent>()
                 .OneFrame<CorrectAnswerEvent>()
                 .OneFrame<WrongAnswerEvent>()
                 .OneFrame<GameOverEvent>()
                 .OneFrame<RestartGameEvent>()
                 .OneFrame<LineDrawData>()
                 .OneFrame<ClearGridEvent>()
                 .OneFrame<UpdateDIndexEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(difficultyConfiguration)
                .Inject(gameFieldConfiguration)
                .Inject(spritesContainer)
                .Inject(timersContainer)
                .Inject(LinesGenerator)
                .Inject(BrezenheimDataContainer)
                .Inject(GameState)
                .Inject(EndgamePresenter)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}