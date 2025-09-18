using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private Transform _playerTrans;
    private Transform _zombiesParent;
    private ZombieManager _zombieManager;
    private WaitForSeconds _waitForSecondsToSpawn;      // caching for performance
    private List<Transform> _zombieSpawnPoints = new();

    void Start()
    {
        // cache zombie spawn points
        foreach (Transform child in transform) _zombieSpawnPoints.Add(child);

        if (_zombieSpawnPoints == null || _zombieSpawnPoints.Count == 0)
        {
            Debug.Log("No Zombie Spawn Points found");
            gameObject.SetActive(false);
            return;
        }

        // fetching
        _playerTrans = FindAnyObjectByType<PlayerController>().transform;
        _zombieManager = ZombieManager.Instance;
        _zombiesParent = GameObject.FindWithTag(Tags.ZombiesParent).transform;

        if (_playerTrans == null || _zombieManager == null)
        {
            Debug.Log("No Player or ZombieManager found");
            gameObject.SetActive(false);
            return;
        }

        _waitForSecondsToSpawn = new(_zombieManager.SpawnInterval);

        // start spawning zombies
        StartCoroutine(SpawnZombies());
    }

    private void SpawnZombie(Transform spawnPoint)
    {
        GameObject zombie = Instantiate(_zombieManager.GetRandomZombiePrefab(), spawnPoint.position, spawnPoint.rotation, _zombiesParent);
        if (zombie.TryGetComponent(out ZombieController zombieController))
        {
            zombieController.SetMoveDirection((_playerTrans.position - spawnPoint.position).normalized);
            zombieController.SetMoveSpeed(_zombieManager.ZombieMoveSpeed);
            // zombieController.OnZombieHit += DeleteZombie;
        }
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            Transform spawnPoint = _zombieSpawnPoints[Random.Range(0, _zombieSpawnPoints.Count)];
            SpawnZombie(spawnPoint);

            yield return _waitForSecondsToSpawn;
        }
    }
}
