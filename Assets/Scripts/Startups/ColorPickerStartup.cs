using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    sealed class ColorPickerStartup : MonoBehaviour
    {
        [SerializeField] EcsUiEmitter _ecsUiEmitter;
        EcsWorld _world;
        EcsSystems _systems;

        public GameModeConfiguration GameModeConfiguration;
        public DifficultyConfiguration difficultyConfiguration;
        public ImageHolderContainer ImageHolderContainer;
        public SoundsContainer SoundsContainer;
        public AudioPlayer AudioPlayer;
        public EndgameScreenPresenter EndgamePresenter;
        public TutorialScreenPresenter TutorialPresenter;
        public ProgressBar ProgressBar;
        public UiScreenContainer ScreenContainer;
        public Slider Slider;

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
            var pausableSystems = new List<string> 
            {
                "UpdateStopwatches",
                "OnSliderValueChange",
                "CheckColorPickerClick"
            };

            _systems
                 // register your systems here, for example:
                 .Add(new CheckPauseClickSystem())
                 .Add(new CheckRestartClickSystem())

                 //InitSystems go here:
                 .Add(new LoadPlayerSystem())
                 .Add(new CreateGameplayEventReceiverSystem())
                 .Add(new SetDifficultySystem())
                 .Add(new CreateStatDataTrackerSystem())
                 .Add(new CreateGameModeDataContainerSystem())
                 .Add(new CreateProgressBarSystem())
                 .Add(new LoadTutorialMessageSystem())
                 .Add(new CreateColorContainerSystem())
                 .Add(new CreateColorDataSystem())
                 .Add(new LaunchGameplayLoopSystem())
                 //The rest of the systems go here:
                 .Add(new UpdateStopwatchesSystem(), "UpdateStopwatches")
                 .Add(new ResetColorsDataSystem())
                 .Add(new ResetColorPickerProgressBarSystem())
                 .Add(new ResetStopwatchTimeSystem())
                 .Add(new ResetStatTrackerSystem())
                 .Add(new StartGameSystem())
                 .Add(new OnSliderValueChangeSystem(), "OnSliderValueChange")
                 .Add(new CheckColorPickerClickSystem(), "CheckColorPickerClick")
                 .Add(new LaunchStatTrackerStopwatchSystem())
                 .Add(new CheckColorPickerAnswerSystem())
                 .Add(new UpdateStatDataSystem())
                 .Add(new UpdateProgressBarSystem())
                 .Add(new GameOverOnTimerEndSystem())
                 .Add(new DisableStopwatchOnGameOverSystem())
                 .Add(new ShowEndgameScreenSystem())
                 .Add(new PauseSystem(_systems, pausableSystems))
                 .Add(new UnpauseSystem(_systems, pausableSystems))
                 .Add(new EnqueueCorrectAnswerAudioClipSystem())
                 .Add(new EnqueueWrongAnswerAudioClipSystem())
                 .Add(new EnqueueGameOverAudioClipSystem())
                 .Add(new PlayAudioClipSystem())
                 .InjectUi(_ecsUiEmitter)

                 // register one-frame components (order is important), for example:
                 .OneFrame<ColorChosenEvent>()
                 .OneFrame<StartGameEvent>()
                 .OneFrame<CorrectAnswerEvent>()
                 .OneFrame<WrongAnswerEvent>()
                 .OneFrame<GameOverEvent>()
                 .OneFrame<RestartGameEvent>()
                 .OneFrame<PauseEvent>()
                 .OneFrame<UnpauseEvent>()
                 .OneFrame<UpdateTextEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(GameModeConfiguration)
                .Inject(difficultyConfiguration)
                .Inject(ImageHolderContainer)
                .Inject(SoundsContainer)
                .Inject(AudioPlayer)
                .Inject(EndgamePresenter)
                .Inject(TutorialPresenter)
                .Inject(ProgressBar)
                .Inject(ScreenContainer)
                .Inject(Slider)
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
