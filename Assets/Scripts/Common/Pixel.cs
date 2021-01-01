using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] public GameObject pixel_empty;

    public Position Position { get; set; }

    public bool IsActive() => !pixel_empty.activeSelf;

    public void SetState(bool state) => pixel_empty.SetActive(!state);

    public void OnMouseDown()
    {
        SendMessageUpwards("GameCheck", this, SendMessageOptions.RequireReceiver);
    }
}



