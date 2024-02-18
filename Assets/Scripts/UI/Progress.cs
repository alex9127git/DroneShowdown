using System;
using UnityEngine;

[Serializable]
public class ProgressData
{
    [SerializeField] public int Tokens;

    public static ProgressData GetEmptyProgressData()
    {
        ProgressData data = new ProgressData();
        data.Tokens = 0;
        return data;
    }
}

public class Progress : MonoBehaviour
{
    [SerializeField] public ProgressData Data;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else Destroy(gameObject);
    }

    public void AddTokens(int amount)
    {
        Data.Tokens += amount;
        Save();
    }

    private void Save()
    {
        ProgressData progressData = Data;

        string json = JsonUtility.ToJson(progressData);
        PlayerPrefs.SetString("Progress", json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Progress"))
        {
            string json = PlayerPrefs.GetString("Progress");
            Data = JsonUtility.FromJson<ProgressData>(json);
        }
        else
        {
            Data = ProgressData.GetEmptyProgressData();
        }
        Save();
    }
}
