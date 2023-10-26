using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    private void LateUpdate()
    {
        Vector3 targetPosition = _player.transform.position;

        if(targetPosition.x != 0)
        {
            targetPosition.x *= 0.5f;
        }

        transform.position = targetPosition;
    }
}
