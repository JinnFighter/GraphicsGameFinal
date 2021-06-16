using UnityEngine;

namespace Pixelgrid
{
    [RequireComponent(typeof(SceneLoader))]
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] private ModeDataBuilder _modeDataBuilder;
        [SerializeField] private SoundController _soundController;
        [SerializeField] private UiScreenContainer _uiScreenContainer;
        private SceneLoader _sceneLoader;

        void Start()
        {
            _sceneLoader = GetComponent<SceneLoader>();
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

        public void PopPanel()
        {
            _uiScreenContainer.Top().SetActive(false);
            _uiScreenContainer.Pop();
            if (_uiScreenContainer.GetCount() > 0)
                _uiScreenContainer.Top().SetActive(true);
        }

        public void PushPanel(GameObject panel)
        {
            if (_uiScreenContainer.GetCount() > 0)
                _uiScreenContainer.Top().SetActive(false);
            
            panel.SetActive(true);
            _uiScreenContainer.Push(panel);
        }
    }
}
