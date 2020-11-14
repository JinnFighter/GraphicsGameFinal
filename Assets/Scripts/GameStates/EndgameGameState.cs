using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameGameState : GameState
{
    [SerializeField] private ScoreKeeper _scoreKeeper;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private Text originalText;
    private List<Text> _texts;

    void Awake()
    {
        _texts = new List<Text>();
    }

    public override void Init()
    {
        var playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
        var score = _scoreKeeper.Score;
        var leaderboard = GetComponent<Leaderboard>();
        leaderboard.AddScore(playerName, score);
        var firstMember = leaderboard.Container.boardMembers[0];
        originalText.text = $"{firstMember.name} {firstMember.score}";

        var boardMembers = leaderboard.Container.boardMembers;
        for (var i = 0; i < boardMembers.Count - 1; i++)
        {
            var text = Instantiate(originalText);
            text.transform.SetParent(originalText.transform.parent);
            _texts.Add(text);
        }

        for(var i = 1; i < boardMembers.Count; i++)
            _texts[i].text = $"{boardMembers[i].name} {boardMembers[i].score}";

        endgameScreen.SetActive(true);
    }

    public override void OnDelete()
    {
        endgameScreen.SetActive(false);
        foreach(var text in _texts)
        {
            text.transform.SetParent(null);
            Destroy(text);
        }
        _texts.Clear();
    }
}
