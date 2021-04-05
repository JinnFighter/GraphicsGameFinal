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
                 .Add(new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new GenerateTimersSystem())
                 .Add(new SetGameplayTimerStartTimeSystem())
                 .Add(new StartGameSystem())
                 .Add(new LaunchGameplayTimerSystem())
                 .Add(new UpdateTimersSystem())
                 .Add(new UpdateStopwatchesSystem())
                 .Add(new GameOverOnTimerEndSystem())
                 .Add(new CheckClickSystem())
                 .Add(new UpdatePixelSystem())
                 .InjectUi(_ecsUiEmitter)
                // .Add (new TestSystem2 ())

                // register one-frame components (order is important), for example:
                 .OneFrame<PixelClickedEvent>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<TimerEndEvent>()
                 .OneFrame<GameOverEvent>()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject(difficultyConfiguration)
                .Inject(gameFieldConfiguration)
                .Inject(spritesContainer)
                .Inject(timersContainer)
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