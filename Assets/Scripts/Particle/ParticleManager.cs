using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [SerializeField] private ParticleSystem _collectCoinParticle;
    [SerializeField] private ParticleSystem _hitObstacleParticle;
    [SerializeField] private Vector3 _instantiateOffset = new(0f, 0.6f, 0f);

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ParticleManager!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void InstantiateCollectCoinParticle(Vector3 instantiatePosition)
    {
        Instantiate(_collectCoinParticle, instantiatePosition + _instantiateOffset, Quaternion.identity);
    }

    public void InstantiateHitObstacleParticle(Vector3 instantiatePosition)
    {
        Instantiate(_hitObstacleParticle, instantiatePosition + _instantiateOffset, Quaternion.identity);
    }
}
