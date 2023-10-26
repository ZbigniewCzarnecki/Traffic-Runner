using TMPro;
using UnityEngine;

public class CountdownToStartUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI _countdownText;
    private Animator _animator;

    private int _previousCountdownNumber;

    [Header("Feedback")]
    [SerializeField] private AudioClip _countdownAudioClip;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer);
        _countdownText.text = countdownNumber.ToString("0");

        if (_previousCountdownNumber != countdownNumber)
        {
            _previousCountdownNumber = countdownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
            AudioManager.Instance.PlaySound(_countdownAudioClip);
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}