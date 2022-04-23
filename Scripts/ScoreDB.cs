using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName="HS",menuName="ScoreDB")]
public class ScoreDB: ScriptableObject
{
    private const string FILENAME = "DB.dat";
    public int[] hightscores = new int[10];
    public int index = 0;
    public int lastScore;

    public void SaveToFile()
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME);

        if(!File.Exists(filePath))
        {
            File.Create(filePath);
        }
       
        var json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, json);
    }

    public void LoadDataFromFile()
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME);

        if(!File.Exists(filePath))
        {
            Debug.LogWarning($"File \"{filePath}\" not found!", this);
            return;
        }

        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
    

}