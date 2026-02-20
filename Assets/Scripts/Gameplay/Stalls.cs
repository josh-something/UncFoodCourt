using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Stalls : MonoBehaviour, IPointerClickHandler
{
    public bool isInStock;
    private int _stock;
    public float itemPrice = 30;
    public StallDisplayPanelInfo stalltype;
    public bool isEmpty = true;
    public GameObject stallPopup;

    public void UpdateStall()
    {
        if (!isEmpty)
        {
            SpriteRenderer f = GetComponent<SpriteRenderer>();
            stallPopup.SetActive(true);
            f.color = Color.red;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (isEmpty) return;
        // Show info Panel
        Debug.Log($"Stall ({stalltype.name}) Clicked");
    }
}
