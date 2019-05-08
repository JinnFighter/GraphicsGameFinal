using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvents.QUIT_GAME, LoadScene);
    }
    IEnumerator loadSceneRoutine(string name)
    {
        loadingImageObject.SetActive(true);
        loadingAnim.Play("loadingPixels");
        string curScene = SceneManager.GetActiveScene().name;
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(name);
        sceneLoader.allowSceneActivation = false;
        while(!sceneLoader.isDone)
        {
            
        
            if (sceneLoader.progress == 0.9f)
            {
                loadingAnim.Stop("loadingPixels");
                //loadingImageObject.SetActive(false);
                sceneLoader.allowSceneActivation = true;
            }
            yield return null;
        }
        /*AsyncOperation sceneUnloader = SceneManager.UnloadSceneAsync(curScene);
        while (!sceneUnloader.isDone)
        {
            yield return null;
        }*/
        //loadingImageObject.SetActive(false);
        //yield return sceneLoader;
    }
    public void LoadScene(string name)
    {
        // loadingImage.SetActive(true);
        
        
        StartCoroutine(loadSceneRoutine(name));
        //loadingImageObject.SetActive(false);
        // loadingImage.SetActive(false);
    }
    public void SaveChosenSceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
    }
    public void LoadChosenScene(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.Save();
        LoadScene(sceneToLoad);
    }
}
