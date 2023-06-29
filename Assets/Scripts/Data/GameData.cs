using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public int Coins { get; set; }
    public int Score { get; set;}
    public int BestScore{ get; set; }

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
            coins = Coins,
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

            Coins = data.coins;
            BestScore = data.bestScore;
        }
    }

    public class Data
    {
        public int coins;
        public int bestScore;
    }
}
