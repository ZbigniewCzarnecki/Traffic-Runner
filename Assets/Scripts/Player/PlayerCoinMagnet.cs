using UnityEngine;

public class PlayerCoinMagnet : MonoBehaviour
{
    [SerializeField] private float _magnetRange = 20f;
    [SerializeField] private float _magnetSpeed = 20f;
    [SerializeField] private float _magnetTimerMax = 10f;
    private float _magnetTimer;

    private bool _isMagnetActive;

    private void Update()
    {
        if (!_isMagnetActive)
        {
            return;
        }

        ActiveMagnetTimer();
        AttractCoins();
    }

    private void ActiveMagnetTimer()
    {
        _magnetTimer += Time.deltaTime;
        if (_magnetTimer >= _magnetTimerMax)
        {
            ResetMagnetTimer();
            DeactivateMagnet();
        }
    }

    private void AttractCoins()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, _magnetRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent<Coin>(out Coin coin))
            {
                coin.transform.position = Vector3.Lerp(coin.transform.position, transform.position, _magnetSpeed * Time.deltaTime);
            }
        }
    }

    public void ActivateMagnet()
    {
        _isMagnetActive = true;
    }

    private void DeactivateMagnet()
    {
        _isMagnetActive = false;
    }

    public void ResetMagnetTimer()
    {
        _magnetTimer = 0;
    }
}
