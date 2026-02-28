using UnityEngine;
using System.Collections;
using  TMPro;

public class Burger : MonoBehaviour
{
    
    [SerializeField]
    private float minX,maxX;
    private int maxScore = 1,scoreGiven,minScore = 0;
    
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("TriggerEntered");
        if (other.CompareTag("BurgerZone"))
        {
            Debug.Log("BurgerPlaced");
            BurgerManager.instance.placed++;
            BurgerManager.instance.ChangePlacedText("Placed: ");
        }
        if (other.CompareTag("KillBox"))
        {
            BurgerManager.instance.missed++;
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
    if (transform.position.x >=minX && transform.position.x <=maxX)
            {   Debug.Log("Burger Centerd");
                if(scoreGiven < maxScore){
                    scoreGiven++;
                    BurgerManager.instance.centered+=scoreGiven;}    
                BurgerManager.instance.ChangeCenteredText("Centered: ");
            }
        else
        {
            if (scoreGiven > minScore){
                scoreGiven--;
                BurgerManager.instance.centered-=1;}
            BurgerManager.instance.ChangeCenteredText("Centered: ");
        }  
    }
    void OnTriggerExit2D(Collider2D other) {
        
            BurgerManager.instance.placed--;
            BurgerManager.instance.UpdateText();
        }

}
