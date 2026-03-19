using System;
using UnityEngine;

public class UpdateRiceSprite : MonoBehaviour
{
    public bool useColorSwitching;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!useColorSwitching && sprites.Length != 5)
        {
            Debug.LogWarning("Sprite Renderer has to have 5 Sprites attached, switching to colorSwitching");
            useColorSwitching = true;
        }
        else
        {
            foreach (var sprite in sprites)
            {
                if (sprite == null)
                {
                    Debug.LogWarning("Sprite Renderer has null sprite, switching to colorSwitching");
                    useColorSwitching = true;
                    break;
                }
            }
        }
        UpdateSprite(0);
    }

    public void UpdateSprite(float percentage)
    {
        if (useColorSwitching)
        {
            SwitchColor(percentage);
            return;
        }
        switch (percentage)
        {
            case 0:
                spriteRenderer.sprite = sprites[0];
                break;
            case 25:
                spriteRenderer.sprite = sprites[1];
                break;
            case 50:
                spriteRenderer.sprite = sprites[2];
                break;
            case 75:
                spriteRenderer.sprite = sprites[3];
                break;
            case 100:
                spriteRenderer.sprite = sprites[4];
                break;
            default:
                spriteRenderer.sprite = sprites[0];
                Debug.Log("Unknown percentage: " + percentage);
                break;
        }
    }

    private void SwitchColor(float percentage)
    {
        switch (percentage)
        {
            case 0:
                spriteRenderer.color = Color.white;
                break;
            case 25:
                spriteRenderer.color = new Color(.9f, .9f, .8f, 1);
                break;
            case 50:
                spriteRenderer.color = new Color(.9f, .9f, .7f, 1);
                break;
            case 75:
                spriteRenderer.color = new Color(.9f, .9f, .6f, 1);
                break;
            case 100:
                spriteRenderer.color = new Color(.9f, .9f, .5f, 1);
                break;
            default:
                spriteRenderer.color = Color.black;
                Debug.Log("Unknown percentage: " + percentage);
                break;
        }
    }
}
