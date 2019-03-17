using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSettings : MonoBehaviour
{
    private bool isFullscreen;
    // Start is called before the first frame update
    void Start()
    {
        isFullscreen = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FullscreenToggle()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}
