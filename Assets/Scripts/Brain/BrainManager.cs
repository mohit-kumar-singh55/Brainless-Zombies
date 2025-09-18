using UnityEngine;

public enum BrainColor { Yellow, Magenta, Blue }

public class BrainManager : MonoBehaviour
{
    public static BrainManager Instance { get; private set; }

    [SerializeField] private GameObject[] brainPrefabs;
    [SerializeField] private float spawnInterval = 1f;           // Time interval between brain spawn
    [SerializeField] private float brainMoveSpeed = 2f;

    public GameObject[] BrainPrefabs => brainPrefabs;
    public float SpawnInterval => spawnInterval;
    public float BrainMoveSpeed => brainMoveSpeed;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public GameObject GetRandomBrainPrefab() => brainPrefabs[Random.Range(0, brainPrefabs.Length)];
}