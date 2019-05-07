using System.Collections;
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
            TextAsset _xml_file = Resources.Load<TextAsset>(path);

            XmlSerializer serializer = new XmlSerializer(typeof(BoardMembersContainer));

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
    
    private string path = "Assets/Data/Leaderboards";
    private BoardMembersContainer container;

    
    internal BoardMembersContainer Container { get => container; set => container = value; }
    // Start is called before the first frame update
    void Start()
    {
        //Messenger.AddListener(GameEvents.GAME_OVER, ShowLeaderboard);
        LoadLeaderboard();
        foreach(BoardMember a in container.boardMembers)
        {
            Debug.Log("Profile name: " + a.name + ", Score: " + a.score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLeaderboard()
    {
        container = BoardMembersContainer.LoadBoardMembers(path + "/" + SceneManager.GetActiveScene().name + ".xml");
    }
    public void AddScore(string playerName, int score)
    {
        foreach(BoardMember a in container.boardMembers)
        {
            if(a.name==playerName&&a.score==score)
            {
                return;
            }
        }
        container.boardMembers.Add(new BoardMember(playerName, score));
        container.boardMembers.Sort((i1, i2) => i2.score.CompareTo(i1.score));
        if(container.boardMembers.Count==11)
        {
            container.boardMembers.RemoveAt(container.boardMembers.Count);
        }
        container.Save(path + "/" + SceneManager.GetActiveScene().name + ".xml");
    }
    /*public void ShowLeaderboard()
    {
        string playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
        int score = GetComponent<ScoreKeeper>().Score;
        AddScore(playerName, score);
        endgameScreen.SetActive(true);
    }*/
}
