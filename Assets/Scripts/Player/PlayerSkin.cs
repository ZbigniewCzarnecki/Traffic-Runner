using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public static PlayerSkin Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerSkin " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        ActivateSkin();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsGameOver)
        {
            DestroyCurrentSkin();
        }
    }

    public void ActivateSkin()
    {
        DestroyCurrentSkin();

        GameObject tmp = Instantiate(SkinPreviewManager.Instance.GetSkinPrefab(GameData.Instance.GetLastActiveSkinIndex()), transform);
        tmp.SetActive(true);
        tmp.AddComponent<PlayerAnimations>();
    }

    public void DestroyCurrentSkin()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
