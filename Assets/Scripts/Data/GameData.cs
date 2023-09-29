using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public int InGameCoins { get; set; }
    public int InGameScore { get; set; }

    private int _coins;
    private int _bestScore;
    private int _lastActiveSkinIndex;
    private List<int> _purchasedSkinsIndexList = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one GameData! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadGameData();
    }

    public void IncreseCoins(int coinsToIncrease = 1)
    {
        _coins += coinsToIncrease;
    }

    public void DecreaseCoins(int coinsToDecrease)
    {
        _coins -= coinsToDecrease;
    }

    public void SetCoins(int value)
    {
        _coins = value;
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void IncreseBestScore(int score = 1)
    {
        _bestScore += score;
    }

    public void SetBestScore(int value)
    {
        _bestScore = value;
    }

    public int GetBestScore()
    {
        return _bestScore;
    }

    public void SetLastActiveSkinIndex(int value)
    {
        _lastActiveSkinIndex = value;
    }

    public int GetLastActiveSkinIndex()
    {
        return _lastActiveSkinIndex;
    }

    public void AddNewPurchasedSkinToAList(int skinIdex)
    {
        _purchasedSkinsIndexList.Add(skinIdex);
    }

    public void SetPurchasedSkinList(List<int> activeSkinsIndexList)
    {
        _purchasedSkinsIndexList = activeSkinsIndexList;
    }
    public List<int> GetPurchasedSkinList()
    {
        return _purchasedSkinsIndexList;
    }

    public void UpdateDataBeforeSaving()
    {
        IncreseCoins(InGameCoins);

        if(GetBestScore() < InGameScore)
        {
            SetBestScore(InGameScore);
        }
    }

    public void SaveGameData()
    {
        Data data = new()
        {
            coins = GetCoins(),
            bestScore = GetBestScore(),
            lastActiveSkinIndex = GetLastActiveSkinIndex(),
            skinsIndexList = GetPurchasedSkinList(),
        };

        string dataContent = JsonUtility.ToJson(data, true);

        DataManager.SaveData(dataContent);
    }

    public void LoadGameData()
    {
        string dataContent = DataManager.LoadData();

        if (dataContent != null)
        {
            Data data = JsonUtility.FromJson<Data>(dataContent);

            SetCoins(data.coins);
            SetBestScore(data.bestScore);
            SetLastActiveSkinIndex(data.lastActiveSkinIndex);
            SetPurchasedSkinList(data.skinsIndexList);
        }
    }

    public void DeleteGameData()
    {
        DataManager.DeleteData();
    }

    public class Data
    {
        public int coins;
        public int bestScore;
        public int lastActiveSkinIndex;
        public List<int> skinsIndexList;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameData))]
internal class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var gameData = (GameData)target;

        if (GUILayout.Button("Reset Game Data")) gameData.DeleteGameData();
        if (GUILayout.Button("Add 1000 Coins")) gameData.IncreseCoins(1000);
    }
}
#endif
