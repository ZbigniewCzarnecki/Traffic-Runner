using System.Collections;
using UnityEngine;

public class CoinMagnet : MonoBehaviour, IInteractable
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private MeshRenderer _magnetMesh;

    PlayerCoinMagnet _playerCoinMagnet;

    public void Interact(PlayerInteractions player)
    {
        _playerCoinMagnet = player.GetComponent<PlayerCoinMagnet>();
        _playerCoinMagnet.ResetMagnetTimer();
        _playerCoinMagnet.ActivateMagnet();

        AudioManager.Instance.PlayPickUpCoinSound();
        StartCoroutine(nameof(MagnetBehaviour));
    }

    private IEnumerator MagnetBehaviour()
    {
        _sphereCollider.enabled = false;
        _magnetMesh.enabled = false;

        float waitForActivateTime = 1f;
        yield return new WaitForSeconds(waitForActivateTime);

        _sphereCollider.enabled = true;
        _magnetMesh.enabled = true;
    }
}