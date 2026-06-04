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
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.simulated = true;
        rb.useFullKinematicContacts = true;

        if (type == ItemType.Bomb)
            transform.localScale = new Vector3(1.4f, 1.4f, 1f);
        else
            transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
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
