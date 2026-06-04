using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("World Movement Settings")]
    public float speed = 10f;
    public float xRange = 8f;

    private Rigidbody2D rb;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.useFullKinematicContacts = true;

        if (transform.localScale == Vector3.zero)
        {
            transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
        float targetX = transform.position.x;
        bool keyboardActive = false;

        if (Keyboard.current != null)
        {
            float moveInput = 0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1f;
                keyboardActive = true;
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1f;
                keyboardActive = true;
            }

            if (keyboardActive)
            {
                targetX += moveInput * speed * Time.deltaTime;
            }
        }

        if (!keyboardActive && Mouse.current != null && Camera.main != null)
        {
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.1f)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z));
                targetX = Mathf.MoveTowards(transform.position.x, worldMousePos.x, speed * Time.deltaTime * 3f);
            }
        }

        targetX = Mathf.Clamp(targetX, -xRange, xRange);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
