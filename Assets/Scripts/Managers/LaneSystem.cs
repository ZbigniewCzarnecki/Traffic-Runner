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
                    Debug.Log("Hit A Wall");
                    break;
                }
            case Lane.Mid:
                {
                    _lane = Lane.Left;
                    break;
                }
            case Lane.Right:
                {
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
                    _lane = Lane.Mid;
                    break;
                }
            case Lane.Mid:
                {
                    _lane = Lane.Right;
                    break;
                }
            case Lane.Right:
                {
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

    public void ResetLane()
    {
        _lane = Lane.Mid;
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


#if UNITY_EDITOR
[CustomEditor(typeof(LaneSystem))]
internal class LaneSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Current Lane");
            EditorGUILayout.Space();

            using (new EditorGUI.IndentLevelScope())
            {
                var lane = ((LaneSystem)target).GetCurrentLaneString();
                EditorGUILayout.LabelField("Lane", lane);
                var positionX = ((LaneSystem)target).GetCurrentLanePositionX();
                EditorGUILayout.LabelField("Position X", positionX.ToString());
            }
        }
    }
}
#endif
