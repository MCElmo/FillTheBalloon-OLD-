using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static BinaryFormatter formatter = new BinaryFormatter();
    private static string path = Application.persistentDataPath + "/player.balloonData";


    public static void SavePlayerLevel(int level)
    {
        PlayerData data = new PlayerData(level);
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadLevel()
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File Not Found");
            SavePlayerLevel(1);
            return new PlayerData(1);
        }
    }
}