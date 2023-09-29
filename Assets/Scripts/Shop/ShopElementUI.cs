using UnityEngine;
using UnityEngine.UI;

public class ShopElementUI : MonoBehaviour
{
    private Button _button;
    [SerializeField] private Image _image;

    public void InitializeButton(int skinIndex)
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            ShopUI.Instance.SelectSkin(skinIndex);
            AudioManager.Instance.PlayClickSound();
        });

        _image.sprite = SkinPreviewManager.Instance.GetSkinIconSprite(skinIndex);
    }
}
