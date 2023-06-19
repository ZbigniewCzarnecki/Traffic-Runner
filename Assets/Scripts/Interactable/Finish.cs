using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);
        {
            player.CompleteLevel();
        }
    }
}
