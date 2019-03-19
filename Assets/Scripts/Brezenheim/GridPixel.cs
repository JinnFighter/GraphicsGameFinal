using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPixel : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;
    private bool painted;
    // Start is called before the first frame update
    void Start()
    {
        painted = false;
        pixel_empty.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {

    }
}
