using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    private string _path;
    private string _gameDataPath;

    private void Start()
    {
        _path = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
        _gameDataPath = _path + "GameData.json";

        Init();
    }

    private void Init()
    {
        if (!File.Exists(_gameDataPath))
            SetGameData(new GameData());
    }

    public void SetGameData(GameData data) =>
        SetData(_gameDataPath, data);

    public GameData GetGameData() =>
        GetData<GameData>(_gameDataPath);

    private void SetData<T>(string path, T data) where T : class
    {
        string jsonString = JsonUtility.ToJson(data, true);

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.WriteLine(jsonString);
        }
    }

    private T GetData<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string jsonString = streamReader.ReadToEnd();
                T data = JsonUtility.FromJson<T>(jsonString);
                return data;
            }
        }

        return null;
    }
}

public class GameData
{
    public int levelNumber = 1;
}
