using UnityEngine;

public class RoadTrigger : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractions player)
    {
        RoadSpawner.Instance.IncreaseRoadCounter();
        transform.parent.gameObject.SetActive(false);
    }
}