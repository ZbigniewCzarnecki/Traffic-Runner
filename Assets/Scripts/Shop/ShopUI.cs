using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Action OnShopCloseAction;

    public static ShopUI Instance { get; private set; }

    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _activeButton;
    [SerializeField] private Button _closeShopButton;

    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _costText;

    [SerializeField] private Transform _skinSelectButtonPrefab;
    [SerializeField] private Transform _skinSelectContainer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ShopUI " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _closeShopButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickSound();
            OnShopCloseAction?.Invoke();
            Hide();
        });
    }

    private void Start()
    {
        int skinsAmount = SkinPreviewManager.Instance.GetSkinsAmount();

        for (int i = 0; i < skinsAmount; i++)
        {
            Transform tmp = Instantiate(_skinSelectButtonPrefab, _skinSelectContainer);

            tmp.GetComponent<ShopElementUI>().InitializeButton(i);

            //Activate Purchased Skin
            if (GameData.Instance.GetPurchasedSkinList().Contains(i))
            {
                SkinPreviewManager.Instance.PurchaseSkin(i);
            }
        }

        Hide();
    }

    public void SelectSkin(int skinIndex)
    {
        SkinPreviewManager.Instance.ActivateNewSkin(skinIndex);

        UpdateCostText(SkinPreviewManager.Instance.GetSkinCost(skinIndex).ToString());

        if (!SkinPreviewManager.Instance.IsPurchased(skinIndex))
        {
            _selectButton.gameObject.SetActive(false);
            _activeButton.gameObject.SetActive(false);

            _buyButton.onClick?.RemoveAllListeners();

            _buyButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayClickSound();

                if (GameData.Instance.GetCoins() >= SkinPreviewManager.Instance.GetSkinCost(skinIndex))
                {
                    GameData.Instance.DecreaseCoins(SkinPreviewManager.Instance.GetSkinCost(skinIndex));

                    GameData.Instance.AddNewPurchasedSkinToAList(skinIndex);

                    GameData.Instance.SaveGameData();

                    UpdateCoinText();

                    ActivateSelectButton(skinIndex);

                    SkinPreviewManager.Instance.PurchaseSkin(skinIndex);
                }
            });
        }
        else
        {
            ActivateSelectButton(skinIndex);
        }
    }

    private void ActivateSelectButton(int skinIndex)
    {
        _selectButton.gameObject.SetActive(true);

        if (skinIndex == GameData.Instance.GetLastActiveSkinIndex())
        {
            _activeButton.gameObject.SetActive(true);
        }
        else
        {
            _activeButton.gameObject.SetActive(false);
        }

        _selectButton.onClick?.RemoveAllListeners();

        _selectButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickSound();

            GameData.Instance.SetLastActiveSkinIndex(skinIndex);
            GameData.Instance.SaveGameData();

            _activeButton.gameObject.SetActive(true);

            PlayerSkin.Instance.ActivateSkin();
        });
    }

    private void UpdateCoinText()
    {
        _coinText.text = GameData.Instance.GetCoins().ToString("000000");
    }

    public void UpdateCostText(string costText)
    {
        _costText.text = costText;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        UpdateCoinText();
        SelectSkin(GameData.Instance.GetLastActiveSkinIndex());
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
