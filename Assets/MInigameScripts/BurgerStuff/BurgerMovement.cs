using UnityEngine;
using System.Collections;

public class BurgerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float screenPadding = 0.5f;

    private Vector2 screenBounds;
    private bool movingRight = true;
    private bool isFalling = false;

    private bool canReceiveInput = true; // NEW

    // manages the burger's movements and how you control it (for individual parts)

    void Start()
{
    Camera mainCamera = Camera.main;

        float distance = Mathf.Abs(mainCamera.transform.position.z);
        screenBounds = mainCamera.ScreenToWorldPoint(
        new Vector3(Screen.width, Screen.height, distance)
    );
}

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1f);
        canReceiveInput = true;
    }

    void Update()
    {
        if (!isFalling)
        {
            MoveLeftRight();
        }

        if (canReceiveInput) // only check input after 1 second
        {
            CheckInput();
        }
    }

    void MoveLeftRight()
    {
        float moveStep = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveStep);

            if (transform.position.x >= screenBounds.x - screenPadding)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveStep);

            if (transform.position.x <= -screenBounds.x + screenPadding)
                movingRight = true;
        }
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartFalling();
        }
    }
    void StartFalling()
    {
        isFalling = true;
        canReceiveInput = false;
        StartCoroutine(InputDelay()); // prevent double clicks

        StartCoroutine(BurgerManager.instance.GetBurgerPart());

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }
}