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
        ActivateSkin();
    }

    public void ActivateSkin()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    
        GameObject tmp = Instantiate(SkinPreviewManager.Instance.GetSkinPrefab(GameData.Instance.GetLastActiveSkinIndex()), transform);
        tmp.SetActive(true);
        tmp.AddComponent<PlayerAnimations>();
    }
}
