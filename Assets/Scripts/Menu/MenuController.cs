using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private UiScreenContainer _uiScreenContainer;
    [SerializeField] private GameObject _mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        _uiScreenContainer.Push(_mainMenu);
    }
}
