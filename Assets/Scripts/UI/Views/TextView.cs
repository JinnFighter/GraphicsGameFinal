using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour, ITextView, IActivatable
{
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    public void SetText(string text) => _text.text = text;

    public bool IsActive() => _text.IsActive();

    public void Activate() => _text.gameObject.SetActive(true);

    public void Deactivate() => _text.gameObject.SetActive(false);
}
