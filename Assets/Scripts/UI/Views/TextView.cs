using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour, ITextView
{
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    public void SetText(string text) => _text.text = text;
}
