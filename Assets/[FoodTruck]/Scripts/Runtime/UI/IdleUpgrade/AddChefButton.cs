using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddChefButton : MonoBehaviour
{

    [SerializeField] private Button _button;
    
    public float InitialCost = 40;
    public float CostMultiplier = 1.1f;
    [SerializeField] private string id = "";
    
    public float CurrentCost => Mathf.RoundToInt(InitialCost * Mathf.Pow(CostMultiplier, itemLevel));
        
    private int itemLevel
    {
        get
        {
            return PlayerPrefs.GetInt($"{id}Level", 1);
        }
        set
        {
            PlayerPrefs.SetInt($"{id}Level", value);
        }
    }
    
    public int ItemLevel => itemLevel;

    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text levelText;
    
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
        SetPrice();
    }

    private void OnClick()
    {
        if (HasEnoughMoney())
        {
            ChefManager.Instance.CreateChef();

            MoneyManager.Instance.DecreaseMoney(CurrentCost);

            //AudioManager.Instance.PlayNormalSound(2);
            
            itemLevel += 1;
        }

        SetPrice();
    }

    private void SetPrice()
    {
        costText.SetText(CurrentCost.ToString());
        SetLevel();
        SetAvailable();

        if (HasEnoughMoney())
        {
            _button.image.DOFade(1f, 0.25f).SetEase(Ease.Linear);
        }
        else
        {
            _button.image.DOFade(0.5f, 0.25f).SetEase(Ease.Linear);
        }
    }

    private void SetAvailable()
    {
        if (ChefManager.Instance.CountFull())
            _button.interactable = false;
        else
            _button.interactable = true;
    }

    private void SetLevel()
    {
        levelText.SetText("LVL" + itemLevel);
    }

    private bool HasEnoughMoney()
    {
        return MoneyManager.Instance.Money >= CurrentCost;
    }
    
}
