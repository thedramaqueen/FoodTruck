using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOrderTransform : Singleton<SendOrderTransform>
{
    //public bool isOccupied;

    public List<Chef> chefList = new List<Chef>();
    
    private void OnTriggerEnter(Collider other)
    {
        Tray tray = other.GetComponent<Tray>();
        if (tray != null)
        {
            if (tray.isEmpty)
            {
                if (chefList.Count > 0)
                {
                    chefList[0].currentOrderProduct.GoToOwner(tray.transform, new Vector3(0, 0.15f, 0));
                    chefList[0].currentOrderProduct.currentTray = tray;
                    //Chef.Instance.currentProduct.GoToOwner(tray.transform, new Vector3(0, 0.15f, 0));

                    tray.currentOrderProduct = chefList[0].currentOrderProduct;
                    //tray.currentProduct = Chef.Instance.currentProduct;
                    tray.isEmpty = false;
                    //isOccupied = false;
                
                    chefList[0].CompleteOrder();

                    chefList[0].chefOrderController.ControlOrderCount();
                    //EventManager.OnSendOrder.Invoke();
                    RemoveChefFromList();
                    //ChefManager.Instance.ActiveChef().chefOrderController.ControlOrderCount();
                    //ChefOrderController.Instance.ControlOrderCount();
                }
            }
        }
    }

    public void AddChefToList(Chef chef)
    {
        if (!chefList.Contains(chef))
        {
            chefList.Add(chef);
        }
    }

    private void RemoveChefFromList()
    {
        chefList.RemoveAt(0);
    }
    
}
