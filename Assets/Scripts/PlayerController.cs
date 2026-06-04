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
        // Force Z-alignment for 2D collisions
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        Debug.Log("PlayerController (World) is active on: " + gameObject.name);

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.useFullKinematicContacts = true;

        // Ensure scale is not zero
        if (transform.localScale == Vector3.zero)
        {
            transform.localScale = Vector3.one;
            Debug.LogWarning("Player scale was zero! Resetting to (1,1,1).");
        }

        Debug.Log("PlayerController ready. Collider: " + GetComponent<Collider2D>());
    }

    void Update()
    {
        float targetX = transform.position.x;
        bool keyboardActive = false;

        // 1. Check for Keyboard input
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

        // 2. If keyboard is not used, follow the Mouse position
        if (!keyboardActive && Mouse.current != null && Camera.main != null)
        {
            // Only follow mouse if it's moving, to prevent snapping back to the cursor 
            // immediately after letting go of the keyboard
            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.1f)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z));
                
                // Instantly snap to mouse X or smoothly follow it. We will smoothly interpolate 
                // so it feels consistent with the movement speed.
                targetX = Mathf.MoveTowards(transform.position.x, worldMousePos.x, speed * Time.deltaTime * 3f);
            }
        }

        targetX = Mathf.Clamp(targetX, -xRange, xRange);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
