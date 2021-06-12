using UnityEngine;

namespace Pixelgrid
{
    [RequireComponent(typeof(NewSceneLoader))]
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] private ModeDataBuilder _modeDataBuilder;
        [SerializeField] private SoundController _soundController;
        private NewSceneLoader _sceneLoader;

        void Start()
        {
            _sceneLoader = GetComponent<NewSceneLoader>();
        }

        public void LoadLevel()
        {
            var data = _modeDataBuilder.GetResult();
            _sceneLoader.LoadChosenScene(data.Difficulty, data.ModeName);
        }

        public void Click()
        {
            _soundController.PlayButtonClickClip();
        }
    }
}
