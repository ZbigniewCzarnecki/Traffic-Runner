using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player _))
        {
            RoadSpawner.Instance.IncreaseRoadCounter();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
