using System.Collections.Generic;
using UnityEngine;

public class UiScreenContainer : MonoBehaviour
{
    private Stack<GameObject> _uiScreens;

    void Awake()
    {
        _uiScreens = new Stack<GameObject>();
    }

    public void Push(GameObject screen)
    {
        _uiScreens.Push(screen);
        screen.SetActive(true);
    }

    public void Pop()
    {
        var screen = _uiScreens.Pop();
        screen.SetActive(false);
    }
}
