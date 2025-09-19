using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }

    private int _score = 0;
    private ZombieManager zombieManager;

    public int Score => _score;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _score = 0;
    }

    void Start()
    {
        zombieManager = ZombieManager.Instance;

        if (zombieManager == null)
        {
            Debug.Log("No ZombieManager found");
            gameObject.SetActive(false);
            return;
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        Debug.Log("Score: " + _score);

        if (_score % 5 == 0)
        {
            zombieManager.IncreaseMoveSpeed();
            zombieManager.DecreaseSpawnInterval();
        }
    }
}
