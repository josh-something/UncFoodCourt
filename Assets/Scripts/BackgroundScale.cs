using UnityEngine;

public class BackgroundScale : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(ScaleBackground), 0.05f);
    }

    void ScaleBackground()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        float spriteWidth = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
        float spriteHeight = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;

        float scale = Mathf.Max(
            screenWidth / spriteWidth,
            screenHeight / spriteHeight
        );

        transform.localScale = new Vector3(scale, scale, 1f);
    }

}
