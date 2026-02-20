using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Stalls : MonoBehaviour, IPointerClickHandler
{
    public bool isInStock;
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
        Debug.Log("Stall Clicked");
    }
}
