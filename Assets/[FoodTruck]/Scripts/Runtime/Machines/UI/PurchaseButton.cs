using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{

    public Button button;
    public TMP_Text costText;
    public float price = 0;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
        EventManager.OnMoneyDataChange.AddListener(SetAvailable);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
        EventManager.OnMoneyDataChange.RemoveListener(SetAvailable);
    }

    private void Start()
    {
        SetAvailable();
    }
    
    private void OnClick()
    {
        if (HasEnoughMoney())
        {
            MachineController.Instance.ActivateNewMachine();

            MoneyManager.Instance.DecreaseMoney(price);

            //AudioManager.Instance.PlayNormalSound(2);
        }
    }

    private void SetAvailable()
    {
        costText.SetText(price.ToString());

        if (HasEnoughMoney())
        {
            button.image.DOFade(1f, 0.25f).SetEase(Ease.Linear);
        }
        else
        {
            button.image.DOFade(0.5f, 0.25f).SetEase(Ease.Linear);
        }
    }
    
    private bool HasEnoughMoney()
    {
        return MoneyManager.Instance.Money >= price;
    }
}
