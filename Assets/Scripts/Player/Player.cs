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

    private void PlayerMovement_OnHitForward(object sender, System.EventArgs e)
    {
        LostLive();
    }

    public void LostLive()
    {
        GameManager.Instance.GameOver();
    }

    public void CompleteLevel()
    {
        GameManager.Instance.CompleteLevel();
    }
}