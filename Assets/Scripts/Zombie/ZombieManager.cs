using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    [SerializeField] private GameObject[] zombiePrefabs;
    [SerializeField] private float spawnInterval = 1f;           // Time interval between brain spawn
    [SerializeField] private float zombieMoveSpeed = 2f;

    public GameObject[] ZombiePrefabs => zombiePrefabs;
    public float SpawnInterval => spawnInterval;
    public float ZombieMoveSpeed => zombieMoveSpeed;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void IncreaseMoveSpeed() => zombieMoveSpeed += zombieMoveSpeed * 0.2f;

    public void DecreaseSpawnInterval() => spawnInterval -= spawnInterval * 0.2f;

    public GameObject GetRandomZombiePrefab() => zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
}
