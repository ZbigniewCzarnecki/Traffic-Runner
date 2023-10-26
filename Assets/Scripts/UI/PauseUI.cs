using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject _quitUI;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            InitializeResumeButton();
        });

        _quitButton.onClick.AddListener(() =>
        {
            InitializeQuitButton();
        });
    }

    private void InitializeResumeButton()
    {
        GameManager.Instance.TogglePauseGame();
        AudioManager.Instance.PlayClickSound();

        Hide();
    }

    private void InitializeQuitButton()
    {
        AudioManager.Instance.PlayClickSound();
        _quitUI.SetActive(true);
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
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
