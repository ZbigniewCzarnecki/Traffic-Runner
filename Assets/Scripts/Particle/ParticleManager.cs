using UnityEngine;
using UnityEngine.Pool;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [SerializeField] private ParticleSystem _collectCoinParticle;
    [SerializeField] private ParticleSystem _hitObstacleParticle;
    [SerializeField] private Vector3 _instantiateOffset = new(0f, 0.6f, 0f);

    ObjectPool<ParticleSystem> _collectParticlePool;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ParticleManager!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _collectParticlePool = new ObjectPool<ParticleSystem>(CreatePooledParticle, OnGetParticleFromPool, OnReleaseParticleFromPool, OnDestroyParticle, true, 10, 50);
    }

    #region CollectParticlePool

    private ParticleSystem CreatePooledParticle()
    {
        ParticleSystem tmp = Instantiate(_collectCoinParticle);
        return tmp;
    }

    private void OnGetParticleFromPool(ParticleSystem particle)
    {
        particle.gameObject.SetActive(true);
    }

    private void OnReleaseParticleFromPool(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
    }

    private void OnDestroyParticle(ParticleSystem particle)
    {
        Destroy(particle.gameObject);
    }

    public void ReleaseParticle(ParticleSystem particle)
    {
        _collectParticlePool.Release(particle);
    }

    #endregion

    public ParticleSystem SpawnCollectCoinParticle(Vector3 instantiatePosition)
    {
        ParticleSystem collectParticle = _collectParticlePool.Get();
        collectParticle.gameObject.transform.SetPositionAndRotation(instantiatePosition + _instantiateOffset, Quaternion.identity);
        return collectParticle;
    }

    public void InstantiateHitObstacleParticle(Vector3 instantiatePosition)
    {
        Instantiate(_hitObstacleParticle, instantiatePosition + _instantiateOffset, Quaternion.identity);
    }
}
