using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event EventHandler OnSwipeLeft;
    public event EventHandler OnSwipeRight;
    public event EventHandler OnSwipeUp;
    public event EventHandler OnSwipeDown;

    [SerializeField] private PlayerInput _gameInput;
    private GameInput _input;

    [SerializeField] private float _swipeThreshold = 1f;
    private Vector2 _pointerDownPosition;
    private Vector2 _pointerUpPosition;

    private bool _isSwiping;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _input = new GameInput();
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Dispose();
    }

    private void Update()
    {
        if (_input.Touch.Press.WasPressedThisFrame())
        {
            _pointerDownPosition = _input.Touch.PointerPosition.ReadValue<Vector2>();
            _isSwiping = true;
        }
        else if (_input.Touch.Press.WasReleasedThisFrame())
        {
            if (_isSwiping)
            {
                _pointerUpPosition = _input.Touch.PointerPosition.ReadValue<Vector2>();
                Vector2 swipeDelta = _pointerUpPosition - _pointerDownPosition;

                if (swipeDelta.magnitude >= _swipeThreshold)
                {
                    swipeDelta.Normalize();

                    float swipeAngle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;

                    if (swipeAngle < 45f && swipeAngle > -45f)
                    {
                        OnSwipeRight?.Invoke(this, EventArgs.Empty);
                    }
                    else if (swipeAngle >= 45f && swipeAngle < 135f)
                    {
                        OnSwipeUp?.Invoke(this, EventArgs.Empty);
                    }
                    else if (swipeAngle >= 135f || swipeAngle <= -135f)
                    {
                        OnSwipeLeft?.Invoke(this, EventArgs.Empty);
                    }
                    else if (swipeAngle > -135f && swipeAngle <= -45f)
                    {
                        OnSwipeDown?.Invoke(this, EventArgs.Empty);
                    }
                }

                _isSwiping = false;
            }
        }
    }

    public bool IsPressWasPerformedThisFrame()
    {
        return _input.Touch.Press.WasPerformedThisFrame();
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
