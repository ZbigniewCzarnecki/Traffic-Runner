using UnityEditor;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public int Coins { get; set; }
    public int AllCoins { get; set; }
    public int Score { get; set; }
    public int BestScore { get; set; }

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

    public void SaveGameData()
    {
        Data data = new()
        {
            allCoins = AllCoins,
            bestScore = BestScore,
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

            AllCoins = data.allCoins;
            BestScore = data.bestScore;
        }
    }

    public void DeleteGameData()
    {
        DataManager.DeleteData();
    }

    public class Data
    {
        public int allCoins;
        public int bestScore;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameData))]
internal class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var resetGameData = (GameData)target;

        if (GUILayout.Button("Reset Game Data")) resetGameData.DeleteGameData();
    }
}
#endif