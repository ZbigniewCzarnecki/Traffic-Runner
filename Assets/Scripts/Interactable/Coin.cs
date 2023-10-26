using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private MeshRenderer _coinMesh;

    public void Interact(PlayerInteractions player)
    {
        GameData.Instance.InGameCoins++;
        GameUI.Instance.UpdateCoinText();

        AudioManager.Instance.PlayPickUpCoinSound();
        ParticleManager.Instance.InstantiateCollectCoinParticle(player.transform.position);

        StartCoroutine(nameof(CoinBehaviour));
    }

    private IEnumerator CoinBehaviour()
    {
        _sphereCollider.enabled = false;
        _coinMesh.enabled = false;

        float waitForActivateTime = 1f;
        yield return new WaitForSeconds(waitForActivateTime);

        _sphereCollider.enabled = true;
        _coinMesh.enabled = true;
    }
}
