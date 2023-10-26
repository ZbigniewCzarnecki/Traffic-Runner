using UnityEngine;

public class Trap : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractions player)
    {
        ParticleManager.Instance.InstantiateHitObstacleParticle(player.transform.position);
        GameManager.Instance.GameOver();
    }
}