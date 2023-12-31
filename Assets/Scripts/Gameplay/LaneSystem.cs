using UnityEditor;
using UnityEngine;

public class LaneSystem : MonoBehaviour
{
    public static LaneSystem Instance { get; private set; }

    private enum Lane
    {
        Left,
        Mid,
        Right
    }

    private Lane _lane = Lane.Mid;
    private Lane _lastLane;

    [SerializeField] private float[] _lanePositionX;
    private float _targetPositionX;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one LaneSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Vector3 GetLeftLanePosition()
    {
        switch (_lane)
        {
            case Lane.Left:
                {
                    _lastLane = Lane.Left;
                    Debug.Log("Hit A Wall");
                    break;
                }
            case Lane.Mid:
                {
                    _lastLane = Lane.Mid;
                    _lane = Lane.Left;
                    break;
                }
            case Lane.Right:
                {
                    _lastLane = Lane.Right;
                    _lane = Lane.Mid;
                    break;
                }
            default:
                {
                    Debug.Log("Unexpected _line value: " + _lane);
                    break;
                }
        }

        _targetPositionX = _lanePositionX[(int)_lane];

        Vector3 targetPosition = new(_targetPositionX, transform.position.y, transform.position.z);
        return targetPosition;
    }

    public Vector3 GetRightLanePosition()
    {
        switch (_lane)
        {
            case Lane.Left:
                {
                    _lastLane = Lane.Left;
                    _lane = Lane.Mid;
                    break;
                }
            case Lane.Mid:
                {
                    _lastLane = Lane.Mid;
                    _lane = Lane.Right;
                    break;
                }
            case Lane.Right:
                {
                    _lastLane = Lane.Right;
                    Debug.Log("Hit A Wall");
                    break;
                }
            default:
                {
                    Debug.Log("Unexpected _line value: " + _lane);
                    break;
                }
        }

        _targetPositionX = _lanePositionX[(int)_lane];

        Vector3 targetPosition = new(_targetPositionX, transform.position.y, transform.position.z);
        return targetPosition;
    }

    public void ResetToLastLane()
    {
        _lane = _lastLane;
    }

    public string GetCurrentLaneString()
    {
        return _lane.ToString();
    }

    public float GetCurrentLanePositionX()
    {
        return _lanePositionX[(int)_lane];
    }
}
