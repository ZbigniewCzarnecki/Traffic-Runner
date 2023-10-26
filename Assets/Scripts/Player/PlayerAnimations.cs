using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string SLIDE = "Slide";
    private const string IS_GROUNDED = "IsGrounded";

    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        _playerMovement.OnSlide += Player_OnSlide;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;

        _playerMovement.OnSlide -= Player_OnSlide;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying)
        {
            _animator.SetTrigger(RUN);
        }

        if (GameManager.Instance.IsGameOver)
        {
            _animator.SetTrigger(IDLE);
        }
    }

    private void Player_OnSlide(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(SLIDE);
    }

    private void Update()
    {
        _animator.SetBool(IS_GROUNDED, _playerMovement.GetIsGrounded());
    }
}
