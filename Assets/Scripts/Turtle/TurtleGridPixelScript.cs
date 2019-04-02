using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGridPixelScript : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;
    [SerializeField] public GameObject gameController;
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
        setPixelState(true);
    }
}
