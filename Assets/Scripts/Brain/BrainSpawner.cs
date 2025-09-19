using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSpawner : MonoBehaviour, IBrainSpawner
{
    [SerializeField] private ZPosition zPosition = ZPosition.Forward;

    private Transform _playerTrans;
    private Transform _brainsParent;
    private BrainManager _brainManager;
    private List<BrainController> _brains = new();
    private WaitForSeconds _waitForSecondsToSpawn;      // caching for performance
    private ShootableZoneManager _shootableZoneManager;

    void OnEnable()
    {
        PlayerController.OnBrainShoot += ShootBrain;
    }

    void OnDisable()
    {
        PlayerController.OnBrainShoot -= ShootBrain;
    }

    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerController>().transform;
        _brainManager = BrainManager.Instance;
        _brainsParent = GameObject.FindWithTag(Tags.BrainsParent).transform;
        _shootableZoneManager = ShootableZoneManager.Instance;

        if (_playerTrans == null || _brainManager == null)
        {
            Debug.Log("No Player or BrainManager found");
            gameObject.SetActive(false);
            return;
        }

        _waitForSecondsToSpawn = new(_brainManager.SpawnInterval);

        // start spawning brains
        StartCoroutine(SpawnBrains());
    }

    private void SpawnBrain()
    {
        GameObject brain = Instantiate(_brainManager.GetRandomBrainPrefab(), transform.position, Quaternion.identity, _brainsParent);
        if (brain.TryGetComponent(out BrainController brainController))
        {
            brainController.SetMoveDirection((_playerTrans.position - transform.position).normalized);
            brainController.SetMoveSpeed(_brainManager.BrainMoveSpeed);
            brainController.OnBrainHit += DeleteBrain;
            AddBrain(brainController);
        }
    }

    public void AddBrain(BrainController brain)
    {
        _brains.Add(brain);
    }

    public void DeleteBrain(BrainController brain)
    {
        _brains.Remove(brain);
    }

    private void ShootBrain(ZPosition zPlayerPosition, XPosition xPosition)
    {
        if (zPosition == zPlayerPosition && _brains.Count > 0)
        {
            ZombieInZone zombieInZone = _shootableZoneManager.FindZombieToShoot(zPlayerPosition, xPosition, _brains[0].BrainColor);

            // shoot at the zombie
            if (zombieInZone.Transform != null && zombieInZone.rb != null)
            {
                BrainController brain = _brains[0];
                brain.ShootItSelf(zombieInZone.Transform.position + new Vector3(0f, 2.8f, 0f));
                _brains.Remove(brain);
            }
        }
    }

    IEnumerator SpawnBrains()
    {
        while (true)
        {
            SpawnBrain();
            yield return _waitForSecondsToSpawn;
        }
    }
}
