using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixelgrid
{
    public class NewSceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingImageObject;

        IEnumerator LoadSceneRoutine(string name)
        {
            _loadingImageObject.SetActive(true);
            _ = SceneManager.GetActiveScene().name;
            var sceneLoader = SceneManager.LoadSceneAsync(name);
            sceneLoader.allowSceneActivation = false;
            while (!sceneLoader.isDone)
            {
                if (sceneLoader.progress == 0.9f)
                {
                    sceneLoader.allowSceneActivation = true;
                }
                yield return null;
            }
        }

        public void LoadScene(string name) => StartCoroutine(LoadSceneRoutine(name));

        public void LoadChosenScene(int difficulty, string sceneToLoad)
        {
            PlayerPrefs.SetInt("difficulty", difficulty);
            PlayerPrefs.Save();
            LoadScene(sceneToLoad);
        }
    }
}
