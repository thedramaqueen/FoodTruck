using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderProduct : MonoBehaviour
{

    public Customer currentCustomer;
    public Tray currentTray;
    
    public void GoToOwner(Transform parent, Vector3 position)
    {
        transform.SetParent(parent);
        transform.localPosition = position;
    }

    public void SetCustomer(Customer customer)
    {
        currentCustomer = customer;
    }

    public void ControlCustomer()
    {
        if (currentCustomer == null || currentCustomer.isLeft)
        {
            Destroy(gameObject, 0.5f);
            if (currentTray != null)
            {
                currentTray.isEmpty = true;
            }
        }
    }

   
}
