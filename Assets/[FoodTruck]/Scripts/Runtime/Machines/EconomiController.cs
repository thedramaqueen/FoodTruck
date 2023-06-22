using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EconomiData
{
    [SerializeField] public string Id;
    [SerializeField] public bool InitialState = true;
    [SerializeField] public float InitialIncome;

    [SerializeField] public float IncomeValue;
    [SerializeField] public float IncomeMultiplier;
    [SerializeField] public List<float> IncomeMultipliers = new List<float>();
    [SerializeField] public int LastMultiplierIndex = 0;
}
public class EconomiController : MonoBehaviour
{

    // public string id = "";
    //
    // public float initialIncome;
    // public float incomeMultiplier;
    //
    // [SerializeField] private List<float> multipliers = new List<float>();
    //
    // //public float currentIncome => Mathf.RoundToInt(initialIncome * incomeMultiplier);
    // //public float currentIncome => Mathf.RoundToInt(initialIncome * Mathf.Pow(incomeMultiplier, itemLevel));
    //
    // public float currentIncome;
    //
    // public int lastMultiplierIndex
    // {
    //     get => PlayerPrefs.GetInt("IncomeMultiplierIndex", 0);
    //     set => PlayerPrefs.SetInt("IncomeMultiplierIndex", value);
    // }
    //
    // public float lastIncomeValue
    // {
    //     get => PlayerPrefs.GetFloat($"{id}IncomeLevel", initialIncome);
    //     set => PlayerPrefs.SetFloat($"{id}IncomeLevel", value);
    // }
    // private int itemLevel
    // {
    //     get => PlayerPrefs.GetInt($"{id}Level", 0);
    //     set => PlayerPrefs.SetInt($"{id}Level", value);
    // }

    [SerializeField] public EconomiData data = new EconomiData();
    public float CurrentIncome => data.IncomeValue;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        LoadData();
        
        SetIncomeMultiplier();
        SetIncomeCost();
    }
    

    private void SetIncomeMultiplier()
    {
        data.IncomeMultiplier = data.IncomeMultipliers[data.LastMultiplierIndex];
    }
    
   

    public void UpgradeIncome()
    {
        data.InitialState = false;
        SetIncomeMultiplier();
        SetIncomeCost();
        //itemLevel += 1;
        data.LastMultiplierIndex ++;
        data.LastMultiplierIndex =
            data.LastMultiplierIndex >= data.IncomeMultipliers.Count ? 0 : data.LastMultiplierIndex;

        SetIncomeMultiplier();
        SaveData();
    }


    private void LoadData()
    {
        var readData = PlayerPrefs.GetString(data.Id+"Economi", null);

        if (!string.IsNullOrEmpty(readData))
        {
            data = JsonUtility.FromJson<EconomiData>(readData);
        }
        
    }
    private void SaveData()
    {
        var jsonData = JsonUtility.ToJson(data,true);
        PlayerPrefs.SetString(data.Id+"Economi", jsonData);
    }


    private void SetIncomeCost()
    {
        data.IncomeValue *= data.IncomeMultiplier;
        data.IncomeValue = data.IncomeValue % 1 >= 0.5f ? Mathf.Ceil(data.IncomeValue) : Mathf.Floor(data.IncomeValue);

        if (data.InitialState)
        {
            data.IncomeValue = data.InitialIncome;
        }
    }
}
