using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("World Movement Settings")]
    public float speed = 10f; 
    public float xRange = 8f;

    void Start()
    {
        Debug.Log("PlayerController (World) is active on: " + gameObject.name);
        
        // Ensure there is a Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        // Check for collider more safely
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("Player is missing a Collider2D component on " + gameObject.name + "! Collisions will not work.");
        }
        else
        {
            Debug.Log("Player collider found on: " + gameObject.name);
        }
    }

    void Update()
    {
        float moveInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveInput = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveInput = 1f;
        }

        float targetX = transform.position.x + moveInput * speed * Time.deltaTime;
        targetX = Mathf.Clamp(targetX, -xRange, xRange);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
