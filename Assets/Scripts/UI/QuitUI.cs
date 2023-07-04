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
            //I'm not sure if i want to collect all coins and score while leaving run
            //GameData.Instance.AllCoins += GameData.Instance.Coins;
            //GameData.Instance.SaveGameData();

            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        _denyButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
