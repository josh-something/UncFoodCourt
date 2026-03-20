using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IngredientMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float horizontalBounds = 3f;

    [Header("State")]
    private bool movingRight = true;
    private bool isDropped = false;
    private bool hasFinished = false;

    [Header("References")]
    private Rigidbody2D rb;
    private BurgerMiniGame miniGame;


    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // no gravity while moving
        miniGame = Object.FindFirstObjectByType<BurgerMiniGame>();
    }

    private void Update()
    {
        if (isDropped) return;

        HandleMovement();
        HandleDropInput();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasFinished) return;

        if (collision.gameObject.CompareTag("Ingredient") || collision.gameObject.CompareTag("StackBase"))
        {
            Landed();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFinished) return;

        if (other.CompareTag("DeadZone"))
        {
            Fell();
        }
    }
    #endregion

    #region Movement
    private void HandleMovement()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > horizontalBounds)
        {
            movingRight = !movingRight;
        }
    }

    private void HandleDropInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Drop();
        }
    }

    private void Drop()
    {
        isDropped = true;
        rb.gravityScale = 3f; // enable falling
        Debug.Log($"Ingredient dropped: {gameObject.name}");
        // Next ingredient will spawn automatically after landing/fall
    }
    #endregion

    #region Landing & Falling
    private void Landed()
    {
        hasFinished = true;
        Debug.Log($"Ingredient landed: {gameObject.name}");
        miniGame?.IngredientLanded(this);
    }

    private void Fell()
    {
        hasFinished = true;
        Debug.Log($"Ingredient fell into DeadZone: {gameObject.name}");
        miniGame?.IngredientFell(this);
        Destroy(gameObject);
    }
    #endregion
}