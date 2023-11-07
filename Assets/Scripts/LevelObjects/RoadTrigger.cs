using UnityEngine;

public class RoadTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Road _road;

    public void Interact(PlayerInteractions player)
    {
        _road.ReleaseRoad();
    }
}
