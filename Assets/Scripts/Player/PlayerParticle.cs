using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickUpCoinParticle;
    [SerializeField] private ParticleSystem _hitObstacleParticle;
    [SerializeField] private Vector3 _offset = new(0f, 0.6f, 0f);

    public void InstantiatePickUpCoinParticle()
    {
        Instantiate(_pickUpCoinParticle, transform.position + _offset, Quaternion.identity);
    }

    public void InstantiateHitObstacleParticle()
    {
        Instantiate(_hitObstacleParticle, transform.position + _offset, Quaternion.identity);
    }
}
