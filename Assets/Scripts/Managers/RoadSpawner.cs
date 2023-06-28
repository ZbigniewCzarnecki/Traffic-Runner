using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    private const float SPAWN_OFFSET = 165f;

    public static RoadSpawner Instance { get; private set; }

    [System.Serializable]
    public class RoadPrefab
    {
        public Transform road;
        public int weight;
    }

    [SerializeField] private RoadPrefab[] _roadPrefab;
    private int _totalWeight;

    [SerializeField] private int _roadPrefabsAmountToPool = 100;

    private readonly List<Transform> _pooledRoadPrefabs = new();

    private float _spawnDistanceOffset;

    private int _roadCounter;
    private readonly int _roadCounterMax = 5;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one RoadSpawner " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        CalculateTotalWeight();

        PoolRoadPrefabs();

        ActivateRoadPrefabsFromPool();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (_roadCounter >= _roadCounterMax)
        {
            _roadCounter = 0;
            ActivateRoadPrefabsFromPool(_roadCounterMax);
        }
    }

    private void CalculateTotalWeight()
    {
        _totalWeight = 0;

        for (int i = 0; i < _roadPrefab.Length; i++)
        {
            _totalWeight += _roadPrefab[i].weight;
        }
    }

    private void PoolRoadPrefabs()
    {
        Transform tmp;

        for (int i = 0; i < _roadPrefabsAmountToPool; i++)
        {
            tmp = GenerateRandomRoadPrefabBasedOnWeight();
            tmp.gameObject.SetActive(false);

            _pooledRoadPrefabs.Add(tmp);
        }
    }

    private Transform GenerateRandomRoadPrefabBasedOnWeight()
    {
        int randomWeight = Random.Range(0, _totalWeight);
        int currentTotalWeight = 0;

        for (int i = 0; i < _roadPrefab.Length; i++)
        {
            currentTotalWeight += _roadPrefab[i].weight;
            if (randomWeight < currentTotalWeight)
            {
                return Instantiate(_roadPrefab[i].road, transform.position, Quaternion.identity);
            }
        }

        return null;
    }

    private void ActivateRoadPrefabsFromPool(int roadPrefabsAmount = 20, float spawnOffset = SPAWN_OFFSET)
    {
        for (int i = 0; i < roadPrefabsAmount; i++)
        {
            Transform roadPrefab = GetPooledPlatform();

            _spawnDistanceOffset += spawnOffset;
            Vector3 newSpawnPosition = new(0, 0, _spawnDistanceOffset);

            if(roadPrefab != null)
            {
                roadPrefab.transform.position = newSpawnPosition;
                roadPrefab.gameObject.SetActive(true);
            }
        }
    }

    private Transform GetPooledPlatform()
    {
        for (int i = 0; i < _pooledRoadPrefabs.Count - 1; i++)
        {
            if (!_pooledRoadPrefabs[i].gameObject.activeInHierarchy)
            {
                return _pooledRoadPrefabs[i];
            }
        }

        return null;
    }

    public void IncreaseRoadCounter()
    {
        _roadCounter++;
    }
}
