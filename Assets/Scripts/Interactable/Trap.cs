using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);
        {
            player.LostLive();
        }
    }
}
