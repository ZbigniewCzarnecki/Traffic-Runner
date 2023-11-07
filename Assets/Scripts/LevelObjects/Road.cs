using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Road : MonoBehaviour
{
    private ObjectPool<Road> _roadPool;
    [SerializeField] private Transform[] _spawnPositions;

    private readonly List<Transform> _roadObjectsList = new();

    public void ActivateLevelObjectsOnRoad()
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            Transform levelObject = LevelObjectSpawner.Instance.GetLevelObjectFromPool();
            levelObject.position = _spawnPositions[i].position;
            _roadObjectsList.Add(levelObject);
        }
    }

    public void ReleaseRoad()
    {
        _roadPool.Release(this);
    }

    public void ReleaseLevelObjects()
    {
        for (int i = 0; i < _roadObjectsList.Count; i++)
        {
            LevelObjectSpawner.Instance.ReleaseLevelObject(_roadObjectsList[i]);
        }

        _roadObjectsList.Clear();
    }

    public void SetPool(ObjectPool<Road> roadPool)
    {
        _roadPool = roadPool;
    }
}