using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Color32 chosenColor;
    private Color curColor;
    private bool winCheck = false;
	//private Stopwatch stpwatch;
    private Color32[] colors = { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow };
    //private Color32[] colors = { Color.black, Color.white };
    [SerializeField] public InputField redTextField;
    [SerializeField] public InputField greenTextField;
    [SerializeField] public InputField blueTextField;
    [SerializeField] public Image colorImage;
    [SerializeField] public Image userColorHolder;
    [SerializeField] public Text statusText;
    [SerializeField] public Text scoreText;
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private Text originalText;
    private int iterations;
    private int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        
        //stpwatch = new Stopwatch();
        //stpwatch.Start();
        restart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnGUI()
    {

    }
    public void checkAnswer()
    {
        /*if(chosenColor.b.ToString()==blueTextField.text && !winCheck)
        {
            statusText.text = "Excellent!";
            //score += 1;
            scoreText.text = "Score: " + score.ToString();
            winCheck = true;
			//stpwatch.Stop();
			//UnityEngine.Debug.Log(stpwatch.Elapsed.Seconds.ToString());
        }
        else
        {
            if(winCheck)
            {
                
                statusText.text = "Please restart!";
            }
            else
            {
                statusText.text = "Wrong!";
            } 
        }*/
        if(Math.Abs(chosenColor.b-byte.Parse(blueTextField.text))<5)
        {
            if(Math.Abs(chosenColor.b - byte.Parse(blueTextField.text)) >= 3)
            {
                UnityEngine.Debug.Log("Correct!");
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                winCheck = true;
            }
            else
            {
                if(Math.Abs(chosenColor.b - byte.Parse(blueTextField.text))>=1)
                {
                    UnityEngine.Debug.Log("Correct!");
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 150);
                    winCheck = true;
                }
                else
                {
                    UnityEngine.Debug.Log("Correct!");
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 200);
                    winCheck = true;
                }
            }
            
        }
        else
        {
            UnityEngine.Debug.Log("Wrong");
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            winCheck = false;
        }
        if(winCheck)
        {
            iterations--;
            if(iterations==0)
            {
                //Messenger.Broadcast(GameEvents.GAME_OVER);

                /* string playerName = GetComponent<ProfilesManager>().ActiveProfile.name;
                 int score = GetComponent<ScoreKeeper>().Score;
                 GetComponent<Leaderboard>().AddScore(playerName, score);

                 originalText.text = GetComponent<Leaderboard>().Container.boardMembers[0].name
                     + GetComponent<Leaderboard>().Container.boardMembers[0].score;
                 for (int i = 1; i < GetComponent<Leaderboard>().Container.boardMembers.Count; i++)
                 {
                     playerName = GetComponent<Leaderboard>().Container.boardMembers[i].name;
                     score = GetComponent<Leaderboard>().Container.boardMembers[i].score;
                     Text text = Instantiate(originalText) as Text;
                     text.text = playerName + score;
                     text.transform.SetParent(originalText.transform.parent);
                 }              
             endgameScreen.SetActive(true);*/
                Messenger.Broadcast(GameEvents.GAME_OVER);
               
            }
            else
            {
                chosenColor = colors[UnityEngine.Random.Range(0, colors.Length)];
                redTextField.text = chosenColor.r.ToString();
                greenTextField.text = chosenColor.g.ToString();
                //blueTextField.text = "0";
                colorImage.color = chosenColor;
                //scoreText.text = "Score: " + score.ToString();
                //winCheck = false;
                float b = curColor.b;
                curColor = new Color(chosenColor.r / 255, chosenColor.g / 255, b, 1);
                userColorHolder.color = curColor;
            }
        }
    }
    public void restart()
    {
        switch (difficulty)
        {
            case 0:
                iterations = 5;
                break;
            case 1:
                iterations = 7;
                break;
            case 2:
                iterations = 10;
                break;
            default:
                iterations = 5;
                break;
        }
        chosenColor = colors[UnityEngine.Random.Range(0, colors.Length)];
        redTextField.text = chosenColor.r.ToString();
        greenTextField.text = chosenColor.g.ToString();
        blueTextField.text = "0";
        colorImage.color = chosenColor;
        //scoreText.text = "Score: " + score.ToString();
        winCheck = false;
        curColor = new Color(chosenColor.r/255, chosenColor.g/255, 0,1);
        userColorHolder.color = curColor;
        Messenger.Broadcast(GameEvents.RESTART_GAME);

    } 
    public void SetChosenColor(float component)
    {
        blueTextField.text = component.ToString();
        curColor.b = component/255;
        userColorHolder.color = curColor;

    }
}
