using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public enum ItemType { Coin, Bomb }
    public ItemType type;
    public float fallSpeed = 5f;

    void Start()
    {
        // Force Z-alignment for 2D collisions
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        // Ensure the collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // Configure Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.simulated = true;
        rb.useFullKinematicContacts = true;

        // Ensure scale is not zero
        if (transform.localScale == Vector3.zero)
            transform.localScale = Vector3.one;
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " triggered by: " + other.gameObject.name);

        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Collision confirmed with Player!");
            GameManager instance = GameManager.instance;
            if (instance != null)
            {
                if (type == ItemType.Coin)
                    instance.AddScore(1);
                else
                    instance.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
