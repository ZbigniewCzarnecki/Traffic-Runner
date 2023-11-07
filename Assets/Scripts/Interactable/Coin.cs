using System;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour, IInteractable
{
    private ParticleSystem _collectCoinParticle;
    private ObjectPool<Coin> _coinPool;

    public void Interact(PlayerInteractions player)
    {
        GameData.Instance.InGameCoins++;
        GameUI.Instance.UpdateCoinText();
        AudioManager.Instance.PlayPickUpCoinSound();

        _collectCoinParticle = ParticleManager.Instance.ActivateCollectCoinParticle(player.transform.position);

        float waitForActivateTime = 1f;
        Invoke(nameof(ReleaseCoinParticle), waitForActivateTime);

        ReleaseCoin();
    }

    public void ReleaseCoin()
    {
        CoinSpawner.Instance.ReleaseCoin(this);
    }

    private void ReleaseCoinParticle()
    {
        ParticleManager.Instance.ReleaseParticle(_collectCoinParticle);
    }

    public void SetPool(ObjectPool<Coin> coinPool)
    {
        _coinPool = coinPool;
    }
}
