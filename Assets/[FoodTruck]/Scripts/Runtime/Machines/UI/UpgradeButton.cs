using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    
    [SerializeField] private Button _button;
    [SerializeField] private string id = "";
    [SerializeField] private Machine machine;
    
    public float InitialCost = 40;
    public float CostMultiplier = 1.1f;
    
    public float CurrentCost => Mathf.RoundToInt(InitialCost * Mathf.Pow(CostMultiplier, itemLevel));

    private int lastMultiplierIndex
    {
        get => PlayerPrefs.GetInt("CostMultiplier", 0);
        set => PlayerPrefs.SetInt("CostMultiplier", value);
    }
    private int itemLevel
    {
        get => PlayerPrefs.GetInt($"{id}Level", 1);
        set => PlayerPrefs.SetInt($"{id}Level", value);
    }
    
    public int ItemLevel => itemLevel;

    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text levelText;
    
    public List<float> multipliers = new List<float>();
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        EventManager.OnMoneyDataChange.AddListener(SetPrice);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        EventManager.OnMoneyDataChange.RemoveListener(SetPrice);
    }
    
    private void Start()
    {
        AddMultipliers();
        SetPrice();
    }
    
    private void OnClick()
    {
        if (HasEnoughMoney())
        {
            CostMultiplier = multipliers[lastMultiplierIndex];
            
            machine.UpgradeMachine();
            
            MoneyManager.Instance.DecreaseMoney(CurrentCost);

            //AudioManager.Instance.PlayNormalSound(2);
            
            itemLevel += 1;
            lastMultiplierIndex += 1;
            if (lastMultiplierIndex >= multipliers.Count)
            {
                lastMultiplierIndex = 0;
            }
        }

        SetPrice();
    }
    
    private void SetPrice()
    {
        CostMultiplier = multipliers[lastMultiplierIndex];
        costText.SetText(CurrentCost.ToString());
        SetLevel();

        if (HasEnoughMoney())
        {
            _button.image.DOFade(1f, 0.25f).SetEase(Ease.Linear);
        }
        else
        {
            _button.image.DOFade(0.5f, 0.25f).SetEase(Ease.Linear);
        }
    }

    private void SetLevel()
    {
        levelText.SetText("LVL" + itemLevel);
    }

    private bool HasEnoughMoney()
    {
        return MoneyManager.Instance.Money >= CurrentCost;
    }

    private void AddMultipliers()
    {
        multipliers.Add(1.04f);
        multipliers.Add(1.06f);
        multipliers.Add(1.08f);
        multipliers.Add(1.1f);
        multipliers.Add(1.12f);
        Debug.Log(multipliers);
    }

}
