using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] public GameObject loadingImageObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator loadSceneRoutine(string name)
    {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(name);
        while(!sceneLoader.isDone)
        {
            yield return null;
        }
        loadingImageObject.SetActive(false);
        //yield return sceneLoader;
    }
    public void LoadScene(string name)
    {
        // loadingImage.SetActive(true);
        loadingImageObject.SetActive(true);
        StartCoroutine(loadSceneRoutine(name));
        //loadingImageObject.SetActive(false);
        // loadingImage.SetActive(false);
    }
}
