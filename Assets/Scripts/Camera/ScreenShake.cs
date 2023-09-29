using Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }
    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ScreenShake! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float force = 1f)
    {
        _cinemachineImpulseSource.GenerateImpulse(force);
    }
}