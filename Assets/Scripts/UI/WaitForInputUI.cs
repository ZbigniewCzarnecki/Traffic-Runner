using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitForInputUI : MonoBehaviour
{
    [SerializeField] private Button _shopButton;

    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _allCoinsText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        _bestScoreText.text = "Best Score: " + GameData.Instance.BestScore.ToString("000000");
        _allCoinsText.text = "Coins: " + GameData.Instance.AllCoins.ToString("0");
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
