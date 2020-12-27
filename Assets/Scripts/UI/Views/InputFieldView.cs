using UnityEngine.UI;

public class InputFieldView : TextView
{
    private InputField _inputField;

    // Start is called before the first frame update
    void Start()
    {
        _inputField = GetComponent<InputField>();
    }

    public override void Activate() => _inputField.gameObject.SetActive(true);

    public override void Deactivate() => _inputField.gameObject.SetActive(false);

    public override bool IsActive() => _inputField.IsActive();

    public override void SetText(string text) => _inputField.text = text;
}
