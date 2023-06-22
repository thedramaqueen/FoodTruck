using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class ChefManager : Singleton<ChefManager>
{

    [SerializeField] private GameObject chefPrefab;

    public int chefIndex = 0;
    //public Chef activeChef;
    //public Chef secondChef;
    
    public List<Chef> chefList = new List<Chef>();
    public List<Chef> activeChefs = new List<Chef>();
    public List<Vector3> chefPositions = new List<Vector3>();
    public List<Customer> waitingCustomerList = new List<Customer>();

    private int chefCount
    {
        get => PlayerPrefs.GetInt("ChefCount", 0);
        set => PlayerPrefs.SetInt("ChefCount", value);
    }
    private void Start()
    {
        ActivateChefs();
    }

    private void ActivateChefs()
    {
        for (int i = 0; i <= chefCount; i++)
        {
            var chef = chefList[i];
            chef.gameObject.SetActive(true);
            chef.ActivateChef();
            AddChefToList(chef);
            //var chef = Instantiate(chefPrefab, transform.position, quaternion.identity, transform);
            //chef.GetComponent<Chef>().ActivateChef();
            //AddChefToList(chef.GetComponent<Chef>());
        }
    }

    public void CreateChef()
    {
        chefCount += 1;
        if(chefCount >= chefList.Count)
            return;

        var chef = chefList[chefCount];
        chef.gameObject.SetActive(true);
        chef.ActivateChef();
        AddChefToList(chef);
        ControlWaitingList();
       
        //var chef = Instantiate(chefPrefab, transform.position, quaternion.identity, transform);
        //chef.GetComponent<Chef>().ActivateChef();
        //chefCount += 1;
    }

    /*
    public Chef ActiveChef(int id)
    {
        if (chefCount == 0)
        {
            return activeChef;
        }
        else
        {
            var i = activeChefs.FindIndex(0, x => x.chefID == id);
            return activeChefs[i];   
        }
    }*/

    public void SendAvailableChef(Customer customer)
    {
        chefIndex = FindAvailableChef();
        
        if (chefIndex == -1)
        {
            AddWaitingList(customer);
        }
        else
        {
            customer.chefID = activeChefs[chefIndex].chefID;
            activeChefs[chefIndex].chefOrderController.enabled = true;
            activeChefs[chefIndex].StartTakingOrder(customer);
            //waitingCustomerList.Remove(customer);
        }
        
    }
    
    private int FindAvailableChef()
    {
        return activeChefs.FindIndex(0, x => x.isActive && x.isAvailable);
    }

    private void AddWaitingList(Customer customer)
    {
        if (!waitingCustomerList.Contains(customer))
        {
            waitingCustomerList.Add(customer);
        }
    }

    public void RemoveFromWaitingList(Customer customer)
    {
        if (waitingCustomerList.Contains(customer))
        {
            var index = waitingCustomerList.FindIndex(x=> x == customer);
            waitingCustomerList.RemoveAt(index);
        }
    }

    public void ControlWaitingList()
    {
        if (waitingCustomerList.Count > 0)
        {
            ControlCustomerNull();
            SendAvailableChef(waitingCustomerList[0]);
        }
    }

    private void ControlCustomerNull()
    {
        if (waitingCustomerList[0] == null)
        {
            waitingCustomerList.RemoveAt(0);
        }
    }
    
    private void AddChefToList(Chef chef)
    {
        if(!activeChefs.Contains(chef))
            activeChefs.Add(chef);
    }

    public bool CountFull()
    {
        if (chefCount >= 2)
            return true;
        else
            return false;
    }
}
