using UnityEngine;

namespace Pixelgrid
{
    [RequireComponent(typeof(NewSceneLoader))]
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] private ModeDataBuilder _modeDataBuilder;
        [SerializeField] private SoundController _soundController;
        [SerializeField] private UiScreenContainer _uiScreenContainer;
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
