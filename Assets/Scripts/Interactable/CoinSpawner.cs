using System;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    private const string COIN_LEVEL_OBJECT = "CoinLevelObject";
    public static CoinSpawner Instance { get; private set; }

    [SerializeField] Coin _coin;
    ObjectPool<Coin> _coinPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _coinPool = new ObjectPool<Coin>(CreateCoinPool, OnGetCoin, OnReleaseCoin, OnDestroyCoin, true, 10, 50);

        LevelObjectSpawner.Instance.OnLevelObjectGetAction += LevelObjectSpawner_OnLevelObjectSpawnAction;
        LevelObjectSpawner.Instance.OnLevelObjectReleaseAction += LevelObjectSpawner_OnLevelObjectReleaseAction;
    }

    private void OnDestroy()
    {
        LevelObjectSpawner.Instance.OnLevelObjectGetAction -= LevelObjectSpawner_OnLevelObjectSpawnAction;
        LevelObjectSpawner.Instance.OnLevelObjectReleaseAction -= LevelObjectSpawner_OnLevelObjectReleaseAction;
    }

    private void LevelObjectSpawner_OnLevelObjectSpawnAction(Transform levelObject)
    {
        SpawnCoin(levelObject);
    }

    private void LevelObjectSpawner_OnLevelObjectReleaseAction(Transform levelObject)
    {
        ReleaseCoin(levelObject);
    }

    private void SpawnCoin(Transform levelObject)
    {
        if (levelObject.CompareTag(COIN_LEVEL_OBJECT))
        {
            foreach (Transform child in levelObject)
            {
                if (child.TryGetComponent<CoinSpawnPosition>(out CoinSpawnPosition coinSpawnPosition) && child.childCount == 0)
                {
                    Coin coin = GetCoinFromPool();
                    coinSpawnPosition.SetParentAndPosition(coin);
                }
            }
        }
    }

    #region Pool

    private Coin CreateCoinPool()
    {
        Coin coin = Instantiate(_coin);
        coin.SetPool(_coinPool);
        return coin;
    }

    private void OnGetCoin(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }

    private void OnReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }

    private void OnDestroyCoin(Coin coin)
    {
        Destroy(coin.gameObject);
    }

    #endregion

    public Coin GetCoinFromPool()
    {
        return _coinPool.Get();
    }

    public void ReleaseCoin(Coin coin)
    {
        coin.GetComponentInParent<CoinSpawnPosition>().ResetParent(coin);
        _coinPool.Release(coin);
    }

    public void ReleaseCoin(Transform levelObject)
    {
        if (levelObject.CompareTag(COIN_LEVEL_OBJECT))
        {
            foreach (Transform child in levelObject)
            {
                if (child.childCount > 0 && child.TryGetComponent<CoinSpawnPosition>(out _))
                {
                    Coin coin = child.GetComponentInChildren<Coin>();
                    coin.ReleaseCoin();
                }
            }
        }
    }
}
