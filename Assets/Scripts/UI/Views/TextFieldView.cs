﻿using UnityEngine.UI;

public class TextFieldView : TextView
{
    private Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    public override void SetText(string text) => _text.text = text;

    public override bool IsActive() => _text.IsActive();

    public override void Activate() => _text.gameObject.SetActive(true);

    public override void Deactivate() => _text.gameObject.SetActive(false);
}
