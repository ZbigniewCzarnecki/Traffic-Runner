using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlayerMovement>(out PlayerMovement _);
        {
            Destroy(gameObject);
        }
    }
}
