using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public GameObject loadingImageObject;
    [SerializeField] public Animation loadingAnim;
    private string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        Messenger<string>.AddListener(GameEvents.QUIT_GAME, LoadScene);
    }

    void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvents.QUIT_GAME, LoadScene);
    }

    IEnumerator loadSceneRoutine(string name)
    {
        loadingImageObject.SetActive(true);
        loadingAnim.Play("loadingPixels");
        _ = SceneManager.GetActiveScene().name;
        var sceneLoader = SceneManager.LoadSceneAsync(name);
        sceneLoader.allowSceneActivation = false;
        while(!sceneLoader.isDone)
        {
            if (sceneLoader.progress == 0.9f)
            {
                loadingAnim.Stop("loadingPixels");
                sceneLoader.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadScene(string name) => StartCoroutine(loadSceneRoutine(name));

    public void SaveChosenSceneToLoad(string sceneName) => sceneToLoad = sceneName;

    public void LoadChosenScene(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.Save();
        LoadScene(sceneToLoad);
    }
}