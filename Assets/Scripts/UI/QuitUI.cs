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
            AudioManager.Instance.PlayClickSound();
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        _denyButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickSound();
            gameObject.SetActive(false);
        });
    }
}
