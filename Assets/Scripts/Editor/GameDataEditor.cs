using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
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