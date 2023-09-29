using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private MeshRenderer _coinMesh;

    [Header("Feedback")]
    [SerializeField] private AudioClip _pickUpSound;

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            GameData.Instance.InGameCoins++;

            GameUI.Instance.UpdateCoinText();

            if(_pickUpSound != null)
            {
                AudioManager.Instance.PlaySound(_pickUpSound);
            }

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
