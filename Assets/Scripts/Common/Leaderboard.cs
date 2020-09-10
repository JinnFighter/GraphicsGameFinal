using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Leaderboard : MonoBehaviour
{
    public class BoardMember
    {
        [XmlElement("Name")]
        public string name;
        [XmlElement("Score")]
        public int score;
        public BoardMember(string Name, int Score)
        {
            name = Name;
            score = Score;
        }

        public BoardMember()
        {

        }
    }

    [XmlRoot("LeaderboardCollection")]
    public class BoardMembersContainer
    {
        [XmlArray("Results")]
        [XmlArrayItem("Player")]
        public List<BoardMember> boardMembers;
        
        public static BoardMembersContainer LoadBoardMembers(string path)
        {
            _ = Resources.Load<TextAsset>(path);

            var serializer = new XmlSerializer(typeof(BoardMembersContainer));

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as BoardMembersContainer;
            }
        }
        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(BoardMembersContainer));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
    
    private string path = "Data/Leaderboards";

    internal BoardMembersContainer Container { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        //Messenger.AddListener(GameEvents.GAME_OVER, ShowLeaderboard);
        LoadLeaderboard();
        foreach(var a in Container.boardMembers)
            Debug.Log("Profile name: " + a.name + ", Score: " + a.score);
    }

    public void LoadLeaderboard() => Container = BoardMembersContainer.LoadBoardMembers(path + "/" + SceneManager.GetActiveScene().name + ".xml");

    public void AddScore(string playerName, int score)
    {
        foreach(var a in Container.boardMembers)
        {
            if(a.name == playerName && a.score == score)
                return;
        }

        Container.boardMembers.Add(new BoardMember(playerName, score));
        Container.boardMembers.Sort((i1, i2) => i2.score.CompareTo(i1.score));
        if(Container.boardMembers.Count == 8)
            Container.boardMembers.RemoveAt(Container.boardMembers.Count - 1);
        Container.Save(path + "/" + SceneManager.GetActiveScene().name + ".xml");
    }
}

