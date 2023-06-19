using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("CollisionLayer")]
    [SerializeField] protected LayerMask _platformLayerMask;
    [Header("IsGrounded")]
    [SerializeField] protected float _sphereRadius = 0.3f;
    protected readonly Collider[] _platformCollider = new Collider[1];
    [Header("Raycast")]
    [SerializeField] protected Vector3 _startPositionOffset = new(0f, 0.5f, 0f);
    [SerializeField] protected float _raycastForwardDistance = .5f;
    [SerializeField] protected float _raycastSidesDistance = 4f;

    protected Rigidbody _rb;
    protected CapsuleCollider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    protected bool RaycastForward()
    {
        return Physics.Raycast(transform.position + _startPositionOffset, Vector3.forward, _raycastForwardDistance, _platformLayerMask);
    }

    protected bool RaycastLeft() 
    {   
        return Physics.Raycast(transform.position + _startPositionOffset, Vector3.left, _raycastSidesDistance, _platformLayerMask);
    }

    protected bool RaycastRight()
    {
        return Physics.Raycast(transform.position + _startPositionOffset, Vector3.right, _raycastSidesDistance, _platformLayerMask);
    }

    protected bool IsGrounded()
    {
        return Physics.OverlapSphereNonAlloc(
            transform.position,
            _sphereRadius,
            _platformCollider,
            _platformLayerMask) > 0;
    }

    protected void OnDrawGizmos()
    {
        if (IsGrounded())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, _sphereRadius);

        Debug.DrawRay(transform.position + _startPositionOffset, Vector3.forward * _raycastForwardDistance, RaycastForward() ? Color.red : Color.green);

        Debug.DrawRay(transform.position + _startPositionOffset, Vector3.left * _raycastSidesDistance, RaycastLeft() ? Color.red : Color.green);
        Debug.DrawRay(transform.position + _startPositionOffset, Vector3.right * _raycastSidesDistance, RaycastRight() ? Color.red : Color.green);
    }
}
