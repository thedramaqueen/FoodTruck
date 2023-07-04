using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MachineBase : MonoBehaviour
{
    public GameObject productPrefab;
    public GameObject machine;
    public string productName;
    public Transform chefTransform;
    public Transform productParent;
    public GameObject unlockPanel;
    public GameObject arrowPanel;
    public GameObject lockPanel;
    [FormerlySerializedAs("product")] public OrderProduct orderProduct;

    public EconomiController economiController;
    
    //public Chronometer chronometer;
    //public float defaultMoneyIncome;
    //public float incomeMultiplier;
    //public int lastMultiplierIndex;
    public bool isActive = false;
    //public List<float> multiplierValues = new List<float>();

    //public float CurrentIncome => Mathf.RoundToInt(moneyAmount * Mathf.Pow(incomeMultiplier, itemLevel));
    //public float CurrentCost => Mathf.RoundToInt(defaultMoneyIncome * Mathf.Pow(CostMultiplier, itemLevel));

    public float moneyAmount => economiController.CurrentIncome;
    /*
    public float moneyAmount
    {
        get => PlayerPrefs.GetFloat($"{productName}MachineMoneyAmount", defaultMoneyIncome);
        set => PlayerPrefs.SetFloat($"{productName}MachineMoneyAmount", value);
    }*/

    public float productionTime
    {
        get => PlayerPrefs.GetFloat("MachineProductionTime", 2);
        set => PlayerPrefs.SetFloat("MachineProductionTime", value);
    }
}
