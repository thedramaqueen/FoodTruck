using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : Singleton<MachineController>
{

    [SerializeField] private List<Machine> _machines = new List<Machine>();
    public List<Machine> _activeMachines = new List<Machine>();
    
    public int lastActiveMachine
    {
        get => PlayerPrefs.GetInt("LastActiveMachine", 1);
        set => PlayerPrefs.SetInt("LastActiveMachine", value);
    }

    private void Awake()
    {
        ActivateActiveMachines();
    }

    private void ActivateActiveMachines()
    {
        for (int i = 0; i < lastActiveMachine; i++)
        {
            _machines[i].ActivateMachine();
        }
    }

    public void ActivateNewMachine()
    {
        if(lastActiveMachine >= _machines.Count)
            return;
        
        _machines[lastActiveMachine].ActivateMachine();
        lastActiveMachine += 1;
        CustomerCreator.Instance.UpgradeProducts();
    }

    public void AddMachineToList(Machine machine)
    {
        _activeMachines.Add(machine);
    }

    public void EarnMoney(int index)
    {
        _activeMachines[index].EarnMoney();
    }
}
