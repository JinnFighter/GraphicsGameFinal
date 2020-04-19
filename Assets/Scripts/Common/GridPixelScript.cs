using UnityEngine;

public class GridPixelScript : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;

    public int X { get; set; }
    public int Y { get; set; }

    public void setPixelState(bool state) => pixel_empty.SetActive(!state);

    public void OnMouseDown() => Messenger<GridPixelScript>.Broadcast(GameEvents.GAME_CHECK, this);
}



