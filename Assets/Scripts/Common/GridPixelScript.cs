using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPixelScript : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;
    private int x;
    private int y;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

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
        Messenger<GridPixelScript>.Broadcast(GameEvents.GAME_CHECK,this);
    }
}



