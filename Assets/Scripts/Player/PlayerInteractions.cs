using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Action OnCollideWithPlatformAction;

    [SerializeField] private LayerMask _platformLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((_platformLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            OnCollideWithPlatformAction?.Invoke();
        }
    }
}