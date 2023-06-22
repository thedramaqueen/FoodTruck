using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MachineBase
{
    
    public void CreateOrder()
    {
        var order = Instantiate(productPrefab, productParent);

        product = order.GetComponent<Product>();
    }

    public void ActivateMachine()
    {
        isActive = true;
        machine.SetActive(true);
        unlockPanel.SetActive(false);
        arrowPanel.SetActive(true);
        lockPanel.SetActive(false);
        MachineController.Instance.AddMachineToList(this);
    }

    public void UpgradeMachine()
    {
        economiController.UpgradeIncome();
        //moneyAmount = economiController.currentIncome;
        productionTime -= 0.1f;
        //chronometer.machineProductionTime = productionTime;
    }

    public void EarnMoney()
    {
        MoneyManager.Instance.IncreaseMoney(moneyAmount);
    }

}
