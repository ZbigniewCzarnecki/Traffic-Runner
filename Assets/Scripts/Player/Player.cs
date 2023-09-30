using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    PlayerMovement _playerMovement;
    PlayerParticle _playerParticle;

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There is more than one Player " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _playerMovement = GetComponent<PlayerMovement>();
        _playerParticle = GetComponent<PlayerParticle>();
    }

    private void Start()
    {
        _playerMovement.OnHitForward += PlayerMovement_OnHitForward;
    }

    private void OnDestroy()
    {
        _playerMovement.OnHitForward -= PlayerMovement_OnHitForward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }

    private void PlayerMovement_OnHitForward(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            HitObstacle();
        }
    }

    public void HitObstacle()
    {
        _playerParticle.InstantiateHitObstacleParticle();

        GameManager.Instance.GameOver();
    }

    public void PickUpCoin() 
    {
        GameData.Instance.InGameCoins++;
        GameUI.Instance.UpdateCoinText();

        AudioManager.Instance.PlayPickUpCoinSound();
        _playerParticle.InstantiatePickUpCoinParticle();
    }
}