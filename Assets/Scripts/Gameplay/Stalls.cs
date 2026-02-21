using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Stalls : MonoBehaviour, IPointerClickHandler
{
    #region Variables
    
    public bool isInStock = true;
    public float itemPrice = 30;
    public StallDisplayPanelInfo stalltype;
    public bool isEmpty = true;
    public GameObject stallPopup;
    
    private int _stock = 10;
    
    #endregion

    public void AddStock(int amount)
    {
        _stock += amount;
        switch (_stock)
        {
            case >0:
                _stock = 0;
                goto case 0;
            case 0:
                isInStock = false;
                break;
            default:
                isInStock = true;
                break;
        }
    }
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
