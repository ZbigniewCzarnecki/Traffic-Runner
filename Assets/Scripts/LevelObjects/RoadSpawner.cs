using System;
using UnityEngine;
using UnityEngine.Pool;

public class RoadSpawner : MonoBehaviour
{
    public static RoadSpawner Instance { get; private set; }

    private const float SPAWN_OFFSET = 50f;

    [SerializeField] private Road _road;
    private ObjectPool<Road> _roadPool;

    private float _spawnPosZ;
    private readonly int _roadAmount = 20;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _roadPool = new ObjectPool<Road>(CreateRoadPool, OnGetRoad, OnReleaseRoad, OnDestroyRoad, true, _roadAmount);
    }

    private void Start()
    {
        SpawnRoadOnStart();
    }

    private void SpawnRoadOnStart()
    {
        for (int i = 0; i < _roadAmount; i++)
        {
            _roadPool.Get();
        }
    }

    #region Pool

    private Road CreateRoadPool()
    {
        Road roadToPool = Instantiate(_road);
        roadToPool.SetPool(_roadPool);
        return roadToPool;
    }

    private void OnGetRoad(Road road)
    {
        road.gameObject.SetActive(true);
        road.transform.position = GetNewSpawnPosition();
        road.ActivateLevelObjectsOnRoad();
    }

    private void OnReleaseRoad(Road road)
    {
        _roadPool.Get();

        road.gameObject.SetActive(false);
        road.ReleaseLevelObjects();
    }

    private void OnDestroyRoad(Road road)
    {
        Destroy(road.gameObject);
    }

    #endregion

    private Vector3 GetNewSpawnPosition(float spawnOffset = SPAWN_OFFSET)
    {
        Vector3 spawnPosition = new(0, 0, _spawnPosZ);
        _spawnPosZ += spawnOffset;
        return spawnPosition;
    }
}