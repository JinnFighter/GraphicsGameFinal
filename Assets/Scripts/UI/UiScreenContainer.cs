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
        if(!_uiScreens.Contains(screen))
        {
            _uiScreens.Push(screen);
            screen.SetActive(true);
        }
    }
           

    public void Pop()
    {
        var screen = _uiScreens.Pop();
        screen.SetActive(false);
    }

    public int GetCount() => _uiScreens.Count;

    public GameObject Top() => _uiScreens.Peek();
}
