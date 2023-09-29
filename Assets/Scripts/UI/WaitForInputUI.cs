using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitForInputUI : MonoBehaviour
{
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private Button _shopButton;

    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _allCoinsText;

    private void Awake()
    {
        _shopButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickSound();
            _shopUI.Show();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ShopUI.Instance.OnShopCloseAction += ShopUI_OnShopCloseAction; 

        UpdateUI();
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        ShopUI.Instance.OnShopCloseAction -= ShopUI_OnShopCloseAction;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Hide();
        }
    }

    private void ShopUI_OnShopCloseAction()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _bestScoreText.text = "Best Score: " + GameData.Instance.GetBestScore().ToString("000000");
        _allCoinsText.text = "Coins: " + GameData.Instance.GetCoins().ToString("000000");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
