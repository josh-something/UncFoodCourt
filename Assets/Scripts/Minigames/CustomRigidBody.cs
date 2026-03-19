using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{

    [SerializeField]
    private float fallSpeed;
    private float y;
    // Update is called once per frame
    void Update()
    {
        y = transform.position.y - 1 * fallSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y, 0f);
    }
}
