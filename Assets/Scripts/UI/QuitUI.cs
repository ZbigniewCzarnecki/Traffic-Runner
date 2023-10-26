using UnityEngine;
using UnityEngine.UI;

public class QuitUI : MonoBehaviour
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _denyButton;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(() =>
        {
            InitializeConfirmButton();
        });

        _denyButton.onClick.AddListener(() =>
        {
            InitializeDenyButton();
        });
    }

    private static void InitializeConfirmButton()
    {
        AudioManager.Instance.PlayClickSound();
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    private void InitializeDenyButton()
    {
        AudioManager.Instance.PlayClickSound();
        gameObject.SetActive(false);
    }
}