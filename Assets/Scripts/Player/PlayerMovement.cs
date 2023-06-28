using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerBase
{
    public event EventHandler OnSlide;
    public event EventHandler OnHitForward;

    [Header("Player")]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _speedIncrease = 0.1f;
    [SerializeField] private float _jumpForce = 35f;
    [SerializeField] private float _changeLaneSpeed = 0.12f;
    [Tooltip("Speed limit to prevent the player from jumping off the ramp uncontrollably.")]
    [SerializeField] private float _walkUpSpeedLimit = 0.5f;

    [Header("CoyoteTime")]
    [Tooltip("The amount of time a player can still jump without being further on the ground.")]
    [SerializeField] private float _timeToJumpMax = .2f;
    private float _timeToJump;

    private bool _abbleToMove;
    private bool _isJumping;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        InputManager.Instance.OnSwipeLeft += InputManager_OnSwipeLeft;
        InputManager.Instance.OnSwipeRight += InputManager_OnSwipeRight;
        InputManager.Instance.OnSwipeUp += InputManager_OnSwipeUp;
        InputManager.Instance.OnSwipeDown += InputManager_OnSwipeDown;

        Score.OnScoreTreshold += Score_OnScoreTreshold;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnSwipeLeft -= InputManager_OnSwipeLeft;
        InputManager.Instance.OnSwipeRight -= InputManager_OnSwipeRight;
        InputManager.Instance.OnSwipeUp -= InputManager_OnSwipeUp;
        InputManager.Instance.OnSwipeDown -= InputManager_OnSwipeDown;

        Score.OnScoreTreshold -= Score_OnScoreTreshold;
    }

    private void Update()
    {
        if (RaycastForward())
        {
            OnHitForward?.Invoke(this, EventArgs.Empty);
        }

        if (!_isJumping && _rb.velocity.y > _walkUpSpeedLimit)
        {
            float rbVelocityY = 0.1f;
            _rb.velocity = new(_rb.velocity.x, rbVelocityY, _speed);
        }

        if (!IsGrounded())
        {
            _timeToJump += Time.deltaTime;
        }
        else
        {
            _timeToJump = 0;
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

        if (IsGrounded())
        {
            Jump();
        }

        if (!IsGrounded() && _timeToJump <= _timeToJumpMax)
        {
            Jump();
        }
    }

    private void InputManager_OnSwipeDown(object sender, EventArgs e)
    {
        if (!_abbleToMove)
        {
            return;
        }

        if (!IsGrounded())
        {
            FallDown();
        }

        Slide();
    }

    private void Score_OnScoreTreshold(object sender, EventArgs e)
    {
        _speed += _speedIncrease;
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
        //reset Y velocity before jump (prevent bigger jumps on ramp)
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        //actual Jump
        _rb.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        //_isJumping boolean preventing from not wanted jumps
        StartCoroutine(JumpReset());
    }

    private IEnumerator JumpReset()
    {
        _isJumping = true;
        yield return new WaitForSeconds(.5f);
        _isJumping = false;
    }

    private void FallDown()
    {

        _rb.AddForce(_jumpForce * Vector3.down, ForceMode.Impulse);
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
