using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Color32 chosenColor;
    private int score;
    private bool winCheck = false;
    //private Color32[] colors = { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow };
    private Color32[] colors = { Color.black, Color.white };
    [SerializeField] public InputField redTextField;
    [SerializeField] public InputField greenTextField;
    [SerializeField] public InputField blueTextField;
    [SerializeField] public Image colorImage;
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
        chosenColor = colors[Random.Range(0, colors.Length)];
        redTextField.text = chosenColor.r.ToString();
        greenTextField.text = chosenColor.g.ToString();
        blueTextField.text = "";
        colorImage.color = chosenColor;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        winCheck = false;
    }
}
