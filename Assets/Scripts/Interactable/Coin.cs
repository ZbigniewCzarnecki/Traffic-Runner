using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private MeshRenderer _coinMesh;

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            GameData.Instance.Coins++;
            GameUI.Instance.UpdateCoinText();
            StartCoroutine(nameof(CoinBehaviour));
        }
    }

    private IEnumerator CoinBehaviour() {
        _sphereCollider.enabled = false;
        _coinMesh.enabled = false;

        float waitForActivateTime = 1f;
        yield return new WaitForSeconds(waitForActivateTime);

        _sphereCollider.enabled = true;
        _coinMesh.enabled = true;
    }
}
