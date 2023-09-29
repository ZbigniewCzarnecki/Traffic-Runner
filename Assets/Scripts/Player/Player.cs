using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    PlayerMovement _playerMovement;

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