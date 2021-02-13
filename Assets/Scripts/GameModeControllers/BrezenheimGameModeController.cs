using System.Collections.Generic;
using UnityEngine;

public class BrezenheimGameModeController : GameModeController
{
    [SerializeField] private List<TextView> _views;
    private NewBrezenheimGameMode _mode;

    void Start()
    {
        var gameField = GetComponent<GameFieldController>();
        _mode = new NewBrezenheimGameMode(gameField.Difficulty, gameField);
        GameMode = _mode;
        _mode.DChangedEvent += OnDChanged;
        _mode.DoRestartAction();
    }

    public override void Check(Pixel invoker) 
    {
        if(GameMode.IsActive())
        {
            var eventType = GameMode.Check(invoker);
            OnGameModeEvent(eventType);
        }
    }

    public void OnDChanged(int d)
    {
        foreach(var view in _views)
            view.SetText(d.ToString());
    }

    void OnDestroy()
    {
        _mode.DChangedEvent -= OnDChanged;
    }
}
