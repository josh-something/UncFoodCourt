using UnityEngine;
using System.Collections;
using  TMPro;

public class Burger : MonoBehaviour
{
    //checks if burger is centered and manages the values(?)
    [SerializeField]
    private float minX,maxX;
    private int maxScore = 1,scoreGiven,minScore = 0;
    private bool isStatic = false;
    private bool scoreApplied = false;
    
    void OnEnable()
    {
        isStatic = false;
        scoreApplied = false;
        scoreGiven = 0;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }



    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("BurgerZone"))
        {
            BurgerManager.instance.placed++;
            BurgerManager.instance.ChangePlacedText("Placed: ");
            BurgerManager.instance.CheckMinigameEnd();
            BurgerManager.instance.StartCoroutine(
                BurgerManager.instance.GetBurgerPart()
            );

            
        }

        if (other.CompareTag("KillBox"))
        {
        BurgerManager.instance.missed++;
        BurgerManager.instance.CheckMinigameEnd();

        if (BurgerManager.instance.placed + BurgerManager.instance.missed 
            < BurgerManager.instance.burgerSpawn.TotalParts())
            {
                BurgerManager.instance.burgerSpawn.UnlockSpawn();
                BurgerManager.instance.StartCoroutine(
                    BurgerManager.instance.GetBurgerPart()
                );
            }

        Destroy(gameObject);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        // Centering score — only apply once per placement
        if (!scoreApplied)
        {
            scoreApplied = true;

            if (transform.position.x >= minX && transform.position.x <= maxX)
            {
                if (scoreGiven < maxScore)
                {
                    scoreGiven++;
                    BurgerManager.instance.centered += scoreGiven;
                }
            }
            else
            {
                if (scoreGiven > minScore)
                {
                    scoreGiven--;
                    BurgerManager.instance.centered -= 1;
                }
            }

            // BurgerManager.instance.ChangeCenteredText("Centered: ");
        }

        // Freeze the part after landing
        if (!isStatic)
        {
            isStatic = true;
            StartCoroutine(FreezePart());
        }
    }

    /*void OnTriggerExit2D(Collider2D other) {
        
            BurgerManager.instance.placed--;
            BurgerManager.instance.UpdateText();
        }*/

    IEnumerator FreezePart()
    {
        yield return new WaitForSeconds(1f);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Static;
    }

}
