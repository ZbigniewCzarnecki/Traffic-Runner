using System.IO;
using UnityEngine;

public static class DataManager
{
    private static readonly string FOLDER_PATH = Application.persistentDataPath + "/SavedData/";
    private static readonly string FILE_NAME = "GameData.json";

    public static void SaveData(string fileContent)
    {
        //Create Folder
        if (!Directory.Exists(FOLDER_PATH))
        {
            Directory.CreateDirectory(FOLDER_PATH);
        }

        //Save File
        File.WriteAllText(FOLDER_PATH + FILE_NAME, fileContent);
    }

    public static string LoadData()
    {
        //Load File
        if (File.Exists(FOLDER_PATH + FILE_NAME))
        {
            string saveString = File.ReadAllText(FOLDER_PATH + FILE_NAME);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
