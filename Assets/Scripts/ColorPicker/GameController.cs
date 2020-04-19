using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Color32 chosenColor;
    private Color curColor;
    private bool winCheck = false;
    private Color32[] colors = { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow };
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
        restart();
    }

    public void OnGUI()
    {

    }

    public void checkAnswer()
    {
        if(Math.Abs(chosenColor.b - byte.Parse(blueTextField.text)) < 5)
        {
            if(Math.Abs(chosenColor.b - byte.Parse(blueTextField.text)) >= 3)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                winCheck = true;
            }
            else
            {
                if(Math.Abs(chosenColor.b - byte.Parse(blueTextField.text)) >= 1)
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 150);
                    winCheck = true;
                }
                else
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 200);
                    winCheck = true;
                }
            }
        }
        else
        {
            Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            winCheck = false;
        }
        if(winCheck)
        {
            iterations--;
            if(iterations==0)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);  
            }
            else
            {
                chosenColor = colors[UnityEngine.Random.Range(0, colors.Length)];
                redTextField.text = chosenColor.r.ToString();
                greenTextField.text = chosenColor.g.ToString();
                colorImage.color = chosenColor;
                var b = curColor.b;
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
        winCheck = false;
        curColor = new Color(chosenColor.r / 255, chosenColor.g / 255, 0, 1);
        userColorHolder.color = curColor;
        Messenger.Broadcast(GameEvents.RESTART_GAME);
    }

    public void SetChosenColor(float component)
    {
        blueTextField.text = component.ToString();
        curColor.b = component/255;
        userColorHolder.color = curColor;
    }

    public void SendStartGameEvent()
    {
        if (PlayerPrefs.GetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit") == 1)
        {
            PlayerPrefs.SetInt(GetComponent<ProfilesManager>().ActiveProfile.name + "_" + SceneManager.GetActiveScene().name + "_first_visit", 0);
            PlayerPrefs.Save();
        }
    }
}
