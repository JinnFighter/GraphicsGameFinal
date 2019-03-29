using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorPickerButtonPressed()
    {
        SceneManager.LoadScene("ColorPicker");
    }
    public void BrezenheimButtonPressed()
    {
        SceneManager.LoadScene("Brezenheim");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
