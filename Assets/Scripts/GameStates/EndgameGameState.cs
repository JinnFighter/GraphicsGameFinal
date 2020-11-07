using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameGameState : GameState
{
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private Text originalText;
    private List<Text> texts;

    void Awake()
    {
        texts = new List<Text>();
    }

    public override void Init()
    {
        var leaderboard = GetComponent<Leaderboard>();
        var playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
        var score = GetComponent<ScoreKeeper>().Score;
        leaderboard.AddScore(playerName, score);

        originalText.text = leaderboard.Container.boardMembers[0].name
            + " " + leaderboard.Container.boardMembers[0].score;
        if (texts.Count != 0)
        {
            for (var i = 0; i < texts.Count; i++)
            {
                texts[i].transform.SetParent(null);
                Destroy(texts[i]);
            }
            texts.Clear();
        }

        for (var i = 1; i < leaderboard.Container.boardMembers.Count; i++)
        {
            playerName = leaderboard.Container.boardMembers[i].name;
            score = leaderboard.Container.boardMembers[i].score;
            var text = Instantiate(originalText);
            text.text = playerName + " " + score;
            text.transform.SetParent(originalText.transform.parent);
            texts.Add(text);
        }
        endgameScreen.SetActive(true);
    }

    public override void OnDelete()
    {
        endgameScreen.SetActive(false);
    }
}
