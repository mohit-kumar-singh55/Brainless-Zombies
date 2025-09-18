using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSpawner : MonoBehaviour, IBrainSpawner
{
    private Transform _playerTrans;
    private Transform _brainsParent;
    private BrainManager _brainManager;
    private Queue<BrainController> _brains = new();
    private WaitForSeconds _waitForSecondsToSpawn;      // caching for performance

    public Queue<BrainController> Brains => _brains;

    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerController>().transform;
        _brainManager = BrainManager.Instance;
        _brainsParent = GameObject.FindWithTag(Tags.BrainsParent).transform;

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
        _brains.Enqueue(brain);
    }

    public void DeleteBrain(BrainController brain)
    {
        _brains.Dequeue();
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
