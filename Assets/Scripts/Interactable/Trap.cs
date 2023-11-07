using UnityEngine;
using UnityEngine.Pool;

public class Trap : MonoBehaviour, IInteractable
{
    ObjectPool<Trap> _trapPool;

    public void Interact(PlayerInteractions player)
    {
        ParticleManager.Instance.InstantiateHitObstacleParticle(player.transform.position);
        GameManager.Instance.GameOver();
    }

    public void ReleaseTrap()
    {
        _trapPool.Release(this);
    }

    public void SetPool(ObjectPool<Trap> trapPool)
    {
        _trapPool = trapPool;
    }
}