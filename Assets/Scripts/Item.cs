using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Coin, Bomb }
    public ItemType type;
    public float fallSpeed = 5f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            GameManager instance = GameManager.instance;
            if (instance != null)
            {
                if (type == ItemType.Coin)
                {
                    instance.AddScore(1);
                }
                else
                {
                    instance.TakeDamage(1);
                }
            }
            Destroy(gameObject);
        }
    }
}
