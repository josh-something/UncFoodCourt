using UnityEngine;

public class StallArea : MonoBehaviour
{
    public StallDisplayPanelInfo stallInfo;

    public bool isLocked = true;

    [Header("Visuals")]
    [SerializeField] private GameObject lockOverlay;
    [SerializeField] private GameObject stallContent;
    [SerializeField] private SpriteRenderer stallRenderer;

    private void Start()
    {
        SetupStall();
        UpdateVisual();
    }

     private void SetupStall()
    {
        if (stallInfo != null && stallRenderer != null)
        {
            stallRenderer.sprite = stallInfo.StallImage;
        }
    }

    private void OnMouseDown()
    {
        if (isLocked)
        {
            UIManager.Instance.OpenUnlockPanel(this);
        }
    }

    public void UnlockStall()
    {
        isLocked = false;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (lockOverlay != null)
            lockOverlay.SetActive(isLocked);

        if (stallContent != null)
            stallContent.SetActive(!isLocked);
    }


    
}
