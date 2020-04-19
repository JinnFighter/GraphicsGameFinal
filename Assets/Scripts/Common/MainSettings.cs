using UnityEngine;

public class MainSettings : MonoBehaviour
{
    private bool isFullscreen;

    // Start is called before the first frame update
    void Start()
    {
        isFullscreen = Screen.fullScreen;
    }

    public void FullscreenToggle()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}
