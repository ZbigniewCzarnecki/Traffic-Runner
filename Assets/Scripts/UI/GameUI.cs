using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [SerializeField] private Button _pauseButton;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one GameUI " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _pauseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickSound();
            GameManager.Instance.TogglePauseGame();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        Score.OnScoreChange += Score_OnScoreChange;

        UpdateScoreText();
        UpdateCoinText();

        Hide();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        GameManager.Instance.OnGamePaused -= Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused -= Instance_OnGameUnpaused;

        Score.OnScoreChange -= Score_OnScoreChange;
    }

    private void Instance_OnGamePaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Score_OnScoreChange(object sender, EventArgs e)
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = GameData.Instance.InGameScore.ToString("000000");
    }

    public void UpdateCoinText()
    {
        _coinText.text = GameData.Instance.InGameCoins.ToString("0000");
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
