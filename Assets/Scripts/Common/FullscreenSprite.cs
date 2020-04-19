using UnityEngine;

public class FullscreenSprite : MonoBehaviour
{
    void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        var spriteSize = spriteRenderer.sprite.bounds.size;

        var scale = transform.localScale;
        if(cameraSize.x >= cameraSize.y)         //Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        else                                     //Portrait
            scale *= cameraSize.y / spriteSize.y;

        transform.position = Vector2.zero;      //Optional
        transform.localScale = scale;
    }
}
