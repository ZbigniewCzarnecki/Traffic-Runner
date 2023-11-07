using UnityEngine;

public class CoinSpawnPosition : MonoBehaviour
{
    public void SetParentAndPosition(Coin coin)
    {
        coin.transform.SetParent(transform);
        coin.transform.localPosition = Vector3.zero;
    }

    public void ResetParent(Coin coin)
    {
        coin.transform.SetParent(null);
    }
}
