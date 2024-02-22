using System;
using System.Reflection;
using UnityEngine;

[Serializable]
public class ProgressData
{
    [SerializeField] public int Tokens;
    [SerializeField] public BlockData[] PlayerDrone;
    [SerializeField] public int[] BoughtBlocks;
    [SerializeField] public int[] Sigils;
    [SerializeField] public int[] SigilUpgradeLevels;

    public static ProgressData GetEmptyProgressData()
    {
        ProgressData data = new ProgressData();

        data.Tokens = 0;
        data.PlayerDrone = new BlockData[0];
        data.BoughtBlocks = new int[6] { 0, 0, 0, 0, 0, 0 };
        data.Sigils = new int[4] { 0, 0, 0, 0 };
        data.SigilUpgradeLevels = new int[4] { 0, 0, 0, 0 };
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

    public void SaveDrone(BlockData[] blocks)
    {
        Data.PlayerDrone = blocks;
        Save();
    }

    public BlockData[] LoadDrone()
    {
        return Data.PlayerDrone;
    }

    public int CountBlocksInDrone(int index)
    {
        int c = 0;
        foreach (BlockData bd in Data.PlayerDrone)
        {
            if (bd.TypeIndex == index) c++;
        }
        return c;
    }

    public void BuyBlock(int index, int price)
    {
        if (Data.Tokens >= price)
        {
            Data.BoughtBlocks[index] += 1;
            Data.Tokens -= price;
        }
        Save();
    }

    public int GetBoughtBlocks(int index)
    {
        return Data.BoughtBlocks[index];
    }

    public void AddSigil(int index)
    {
        Data.Sigils[index] += 1;
        Save();
    }

    public void AddSigilUpgrade(int index)
    {
        if (Data.Sigils[index] > Data.SigilUpgradeLevels[index])
        {
            Data.Sigils[index] -= Data.SigilUpgradeLevels[index] + 1;
            Data.SigilUpgradeLevels[index] += 1;
        }
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
