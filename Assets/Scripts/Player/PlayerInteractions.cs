using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }
}