using UnityEngine;

public class Trap : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        player.HitObstacle();
    }
}
