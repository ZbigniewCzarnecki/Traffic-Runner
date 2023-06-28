using TMPro;
using UnityEngine;

public class WaitForInputUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        _bestScoreText.text = "Best Score: " + GameData.Instance.BestScore.ToString("000000");
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
