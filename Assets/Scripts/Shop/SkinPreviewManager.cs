using System;
using UnityEngine;

public class SkinPreviewManager : MonoBehaviour
{
    public static SkinPreviewManager Instance { get; private set; }

    [SerializeField] private SkinData[] _skinPrefabsArray;
    private int _activeSkinIndex;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one SkinPreview " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _skinPrefabsArray.Length; i++)
        {
            _skinPrefabsArray[i].skinPrefab.SetActive(false);
        }

        _activeSkinIndex = GameData.Instance.GetLastActiveSkinIndex();

        _skinPrefabsArray[_activeSkinIndex].skinPrefab.SetActive(true);
    }

    public void ActivateNewSkin(int newSkinIndex)
    {
        _skinPrefabsArray[_activeSkinIndex].skinPrefab.SetActive(false);

        _activeSkinIndex = newSkinIndex;

        _skinPrefabsArray[_activeSkinIndex].skinPrefab.SetActive(true);
    }

    public int GetSkinsAmount()
    {
        return _skinPrefabsArray.Length;
    }

    public int GetSkinCost(int skinIndex)
    {
        return _skinPrefabsArray[skinIndex].cost;
    }

    public bool IsPurchased(int skinIndex)
    {
        return _skinPrefabsArray[skinIndex].purchased;
    }

    public void PurchaseSkin(int skinIndex)
    {
        _skinPrefabsArray[skinIndex].purchased = true;
    }

    public GameObject GetSkinPrefab(int skinIndex)
    {
        return _skinPrefabsArray[skinIndex].skinPrefab;
    }

    public Sprite GetSkinIconSprite(int skinIndex)
    {
        return _skinPrefabsArray[skinIndex].iconSprite;
    }
}

[Serializable]
public class SkinData
{
    public int cost;
    public GameObject skinPrefab;
    public Sprite iconSprite;
    public bool purchased;
}