using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerBase
{
    public event EventHandler OnSlide;
    public event EventHandler OnHitForward;

    [Header("Player")]
    [SerializeField] private float _speed = 25f;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private float _changeLaneSpeed = 0.15f;

    private bool _abbleToMove;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        InputManager.Instance.OnSwipeLeft += InputManager_OnSwipeLeft;
        InputManager.Instance.OnSwipeRight += InputManager_OnSwipeRight;
        InputManager.Instance.OnSwipeUp += InputManager_OnSwipeUp;
        InputManager.Instance.OnSwipeDown += InputManager_OnSwipeDown;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnSwipeLeft -= InputManager_OnSwipeLeft;
        InputManager.Instance.OnSwipeRight -= InputManager_OnSwipeRight;
        InputManager.Instance.OnSwipeUp -= InputManager_OnSwipeUp;
        InputManager.Instance.OnSwipeDown -= InputManager_OnSwipeDown;
    }

    private void Update()
    {
        if (RaycastForward())
        {
            OnHitForward?.Invoke(this, EventArgs.Empty);
        }
    }

    private void FixedUpdate()
    {
        if (!_abbleToMove)
        {
            return;
        }

        RunForward();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            _abbleToMove = true;
        }
        else
        {
            _abbleToMove = false;
            _rb.velocity = Vector3.zero;
        }
    }

    private void InputManager_OnSwipeLeft(object sender, EventArgs e)
    {
        if (!_abbleToMove)
        {
            return;
        }

        MoveLeft();
    }

    private void InputManager_OnSwipeRight(object sender, EventArgs e)
    {
        if (!_abbleToMove)
        {
            return;
        }

        MoveRight();
    }

    private void InputManager_OnSwipeUp(object sender, EventArgs e)
    {
        if (!_abbleToMove)
        {
            return;
        }

        Jump();
    }

    private void InputManager_OnSwipeDown(object sender, EventArgs e)
    {
        if (!_abbleToMove)
        {
            return;
        }

        FallDown();
        Slide();
    }

    private void MoveLeft()
    {
        if (!RaycastLeft())
        {
            Vector3 targetPosition = LaneSystem.Instance.GetLeftLanePosition();
            StartCoroutine(MoveToPosition(targetPosition, _changeLaneSpeed));
        }
    }

    private void MoveRight()
    {
        if (!RaycastRight())
        {
            Vector3 targetPosition = LaneSystem.Instance.GetRightLanePosition();
            StartCoroutine(MoveToPosition(targetPosition, _changeLaneSpeed));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = _rb.position;
        Vector3 targetPositionWithYAndZ = new(targetPosition.x, startPosition.y, startPosition.z);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPositionWithYAndZ, t);
            _rb.MovePosition(new Vector3(newPosition.x, _rb.position.y, _rb.position.z));
            yield return null;
        }

        _rb.MovePosition(new Vector3(targetPosition.x, _rb.position.y, _rb.position.z));
    }

    private void Slide()
    {
        _collider.height = 1f;
        _collider.center = new Vector3(0f, .5f, 0f);

        Invoke(nameof(ResetSlide), 1f);

        OnSlide?.Invoke(this, EventArgs.Empty);
    }

    private void ResetSlide()
    {
        _collider.height = 1.8f;
        _collider.center = new Vector3(0f, 0.9f, 0f);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    private void FallDown()
    {
        if (!IsGrounded())
        {
            _rb.AddForce(_jumpForce * Vector3.down, ForceMode.Impulse);
        }
    }

    private void RunForward()
    {
        _rb.velocity = new(_rb.velocity.x, _rb.velocity.y, _speed);
    }

    public bool GetIsGrounded()
    {
        return IsGrounded();
    }
}
