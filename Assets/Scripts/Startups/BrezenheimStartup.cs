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
                 .Add (new SetDifficultySystem())
                 .Add(new GenerateGameFieldSystem())
                 .Add(new UpdateTimersSystem())
                 .Add(new UpdateStopwatchesSystem())
                 .Add(new CheckClickSystem())
                 .InjectUi(_ecsUiEmitter)
                // .Add (new TestSystem2 ())

                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject(difficultyConfiguration)
                .Inject(gameFieldConfiguration)
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