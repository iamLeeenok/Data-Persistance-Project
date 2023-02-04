using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public string activePlayer;
    public string bestPlayer;
    public int bestScorePoints;
    // Path where to Save player's data
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }


    // Transfer data IO json file
    [System.Serializable]
    class SaveData
    {
        public string bestPlayer;
        public int bestScorePoints;
    }


    // Transfer SaveData to the file
    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.bestPlayer = bestPlayer;
        data.bestScorePoints = bestScorePoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    // Load SaveData from the file
    public void LoadPlayerData()
    {
        string storePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(storePath))
        {
            string json = File.ReadAllText(storePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.bestPlayer;
            bestScorePoints = data.bestScorePoints;
        }
    }
}
