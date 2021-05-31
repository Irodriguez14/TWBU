using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager{

    public static void SavePlayer() {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.mjc";
        try {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            PlayerData data = new PlayerData();
            formater.Serialize(stream, data);
            stream.Close();
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
        }
       
    }

    public static void Load() {
        string path = Application.persistentDataPath + "/Player.mjc";
        Debug.Log("holoa");
        if (File.Exists(path)) {
            GameManager.gameManager.fileExists = true;
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formater.Deserialize(stream) as PlayerData;
            GameManager.gameManager.getBools(data);
            stream.Close();
            //return data;
        }
        else {
            GameManager.gameManager.fileExists = false;
            Debug.LogError("not found");
            GameManager.gameManager.errorLoading();
        }
    }
}
