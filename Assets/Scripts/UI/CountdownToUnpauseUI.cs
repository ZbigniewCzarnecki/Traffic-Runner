using TMPro;
using UnityEngine;

public class CountdownToUnpauseUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    public static CountdownToUnpauseUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _countdownText;
    private Animator _animator;

    private int _previousCountdownNumber;

    [Header("Feedback")]
    [SerializeField] private AudioClip _countdownAudioClip;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one CountdownToUnpauseUI " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToUnpauseTimer);
        _countdownText.text = countdownNumber.ToString("0");

        if (_previousCountdownNumber != countdownNumber)
        {
            _previousCountdownNumber = countdownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
            AudioManager.Instance.PlaySound(_countdownAudioClip);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
