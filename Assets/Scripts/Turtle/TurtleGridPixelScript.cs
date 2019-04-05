using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGridPixelScript : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;
    [SerializeField] public GameObject gameController;
   
    private enum commandsEnum { FORWARD, ROTATE_PLUS, ROTATE_MINUS };
   
    private Hashtable commandsTable = new Hashtable { {'F', commandsEnum.FORWARD}, { '+', commandsEnum.ROTATE_PLUS },
        { '-', commandsEnum.ROTATE_MINUS } };
    private int angle = 90;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setPixelState(bool state)
    {
        pixel_empty.SetActive(!state);
    }
    public void OnMouseDown()
    {
        if(pixel_empty.activeSelf)
        {
            setPixelState(true);
        }
        else
        {
            setPixelState(false);
        }
    }
}
