using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Color32 chosenColor;
    private Color curColor;
    private int score;
    private bool winCheck = false;
    //private Color32[] colors = { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow };
    private Color32[] colors = { Color.black, Color.white };
    [SerializeField] public InputField redTextField;
    [SerializeField] public InputField greenTextField;
    [SerializeField] public InputField blueTextField;
    [SerializeField] public Image colorImage;
    [SerializeField] public Image userColorHolder;
    [SerializeField] public Text statusText;
    [SerializeField] public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
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
        if(chosenColor.b.ToString()==blueTextField.text && !winCheck)
        {
            statusText.text = "Excellent!";
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            winCheck = true;
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
        }
    }
    public void restart()
    {
        chosenColor = colors[UnityEngine.Random.Range(0, colors.Length)];
        redTextField.text = chosenColor.r.ToString();
        greenTextField.text = chosenColor.g.ToString();
        blueTextField.text = "0";
        colorImage.color = chosenColor;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        winCheck = false;

        //Color color = new Color((float)chosenColor.r / 255, (float)chosenColor.g / 255, 0, 1);
        //Color32 color = new Color32(chosenColor.r, chosenColor.g, 0, 1);
        curColor = new Color(chosenColor.r/255, chosenColor.g/255, 0,1);
        userColorHolder.color = curColor;
        //Color32 c = new Color32(chosenColor.r, chosenColor.g, 0, 1);
        //userColorHolder.color = (Color)c;
        //userColorHolder.color = new Color32(chosenColor.r, chosenColor.g, 0, 0);
    } 
    public void SetChosenColor(float component)
    {
        blueTextField.text = component.ToString();
        curColor.b = component/255;
        userColorHolder.color = curColor;
        //byte[] vOut = BitConverter.GetBytes(component);
        //Color c = new Color((float)chosenColor.r, (float)chosenColor.g, component);
        //Color32 c = new Color(chosenColor.r, chosenColor.g, (byte)component);
        //Color32 c = new Color32(chosenColor.r, chosenColor.g, 0, 1);
        //colorImage.color = Color.black;
        //Color color = new Color((float)chosenColor.r / 255, (float)chosenColor.g / 255, component, 1);
       // Color32 color = new Color32(chosenColor.r, chosenColor.g, component, 1);
        //userColorHolder.color = color;
    }
}
