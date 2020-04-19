using UnityEngine;

public class TurtleGridPixelScript : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;
    [SerializeField] public GameObject gameController;
    
    public void setPixelState(bool state) => pixel_empty.SetActive(!state);

    public void OnMouseDown() => setPixelState(pixel_empty.activeSelf);
}