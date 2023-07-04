using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _playerMovement.OnHitForward += PlayerMovement_OnHitForward;
    }

    private void OnDestroy()
    {
        _playerMovement.OnHitForward -= PlayerMovement_OnHitForward;
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
        GameManager.Instance.GameOver();
    }
}