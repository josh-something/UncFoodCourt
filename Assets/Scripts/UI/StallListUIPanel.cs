using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StallListUIPanel : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;

    public Sprite emptySprite;

    private StallArea stall;

    public Sprite lockedSprite;

    public void LockedStall()
    {
        icon.sprite = lockedSprite;
        nameText.text = "Locked Stall";
    }

    public void EmptyStall()
    {
        icon.sprite = emptySprite;
        nameText.text = "Stall Empty";
    }

    public void StallWithFood(StallFoodData food)
    {
        icon.sprite = food.stallFoodIcon;
        nameText.text = food.stallFoodName;
    }

    public void SetStall(StallArea s)
    {
        stall = s;

        if (stall.assignedFood == null)
            EmptyStall();
        else
            StallWithFood(stall.assignedFood);
    }

    public void OnClickSlot()
    {
        if (stall.assignedFood != null)
            StallUIManager.Instance.OpenStallInfoPanel(stall);
    }
}
