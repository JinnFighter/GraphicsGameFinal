using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : Controller
{
    [SerializeField] private GameObject _loadingImageObject;
    [SerializeField] private Animation _loadingAnim;
    private string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        actions.Add(GameEvents.QUIT_GAME, OnQuitGame);
    }

    IEnumerator LoadSceneRoutine(string name)
    {
        _loadingImageObject.SetActive(true);
        _loadingAnim.Play("loadingPixels");
        _ = SceneManager.GetActiveScene().name;
        var sceneLoader = SceneManager.LoadSceneAsync(name);
        sceneLoader.allowSceneActivation = false;
        while (!sceneLoader.isDone)
        {
            if (sceneLoader.progress == 0.9f)
            {
                _loadingAnim.Stop("loadingPixels");
                sceneLoader.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadScene(string name) => StartCoroutine(LoadSceneRoutine(name));

    public void SaveChosenSceneToLoad(string sceneName) => sceneToLoad = sceneName;

    public void LoadChosenScene(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.Save();
        LoadScene(sceneToLoad);
    }

    private void OnQuitGame() => StartCoroutine(LoadSceneRoutine("MainMenu"));
}
