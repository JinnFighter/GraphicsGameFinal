using UnityEngine;

public class MainController : MonoBehaviour, IPausable
{
    [SerializeField] private StatesContainer _statesContainer;

    // Start is called before the first frame update
    void Start()
    {
        _statesContainer.OnStartEvent();
    }

    public void Pause() => _statesContainer.Pause();

    public void Unpause() => _statesContainer.Unpause();
}
