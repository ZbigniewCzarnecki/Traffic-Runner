using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _menuButton;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            InitializePlayButton();
        });
    }

    private static void InitializePlayButton()
    {
        AudioManager.Instance.PlayClickSound();
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver)
        {
            float timeToShowGameOverUI = 0.5f;
            Invoke(nameof(Show), timeToShowGameOverUI);

            _scoreText.text = "Score: " + GameData.Instance.InGameScore.ToString("000000");
            _coinText.text = "Coins: " + GameData.Instance.InGameCoins.ToString("0000");
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
