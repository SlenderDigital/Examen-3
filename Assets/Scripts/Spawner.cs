using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject bombPrefab;
    public float spawnRangeX = 8f;
    public float initialSpawnInterval = 1.5f;
    public float difficultyIncreaseRate = 0.05f;
    public float initialGravity = 5f;
    
    [Header("Bomb Probability")]
    public float initialBombProbability = 0.3f;
    public float maxBombProbability = 0.8f;
    public float bombProbabilityIncreaseRate = 0.01f;

    private float timer;
    private float currentSpawnInterval;
    private float elapsedTime;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            SpawnItem();
            timer = 0f;
            IncreaseDifficulty();
        }
    }

    void SpawnItem()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, 3.5f, 0f);

        float currentBombProbability = Mathf.Min(maxBombProbability, initialBombProbability + (elapsedTime * bombProbabilityIncreaseRate));
        bool isBomb = Random.value < currentBombProbability;
        GameObject prefab = isBomb ? bombPrefab : coinPrefab;

        GameObject newItem = Instantiate(prefab, spawnPos, Quaternion.identity, null);
        Item itemScript = newItem.GetComponent<Item>();
        
        if (itemScript != null)
        {
            itemScript.fallSpeed = initialGravity + (elapsedTime * 0.2f);
        }
    }

    void IncreaseDifficulty()
    {
        currentSpawnInterval = Mathf.Max(0.5f, initialSpawnInterval - (elapsedTime * difficultyIncreaseRate));
    }
}
