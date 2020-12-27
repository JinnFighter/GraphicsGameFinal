using System.Collections.Generic;
using UnityEngine;

public class BrezenheimGameModeController : GameModeController
{
    [SerializeField] private List<TextView> _views;
    private NewBrezenheimGameMode _mode;

    void Awake()
    {
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, Check);
    }

    void Start()
    {
        var gameField = GetComponent<GameFieldController>();
        
        _mode = new NewBrezenheimGameMode(gameField.Difficulty, gameField);
        GameMode = _mode;
        _mode.DChangedEvent += OnDChanged;
        _mode.DoRestartAction();   
    }

    public void Check(Pixel invoker) 
    {
        if(GameMode.IsActive())
            GameMode.Check(invoker);
    }

    public void OnDChanged(int d)
    {
        foreach(var view in _views)
            view.SetText(d.ToString());
    }

    void OnDestroy()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, Check);
        _mode.DChangedEvent -= OnDChanged;
    }
}
