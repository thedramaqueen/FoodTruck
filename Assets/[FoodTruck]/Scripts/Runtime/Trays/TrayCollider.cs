using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayCollider : MonoBehaviour
{
    private Tray _tray;
    private Tray Tray => _tray == null ? _tray = GetComponent<Tray>() : _tray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CustomerCheck"))
        {
            if (!Tray.isEmpty && other.transform.GetComponentInParent<Customer>().isOrdered)
            {
                // Tabakta ki Urunu Musteriye Ver
                if (Tray.currentOrderProduct.currentCustomer == other.transform.GetComponentInParent<Customer>())
                {
                    Debug.Log("dogru musteri");
                    Tray.currentOrderProduct.GoToOwner(other.transform.GetComponentInParent<Customer>().transform.GetChild(0), new Vector3(0, 1, 0.5f));
                    other.transform.GetComponentInParent<Customer>().SetOrderText();
                    Tray.isEmpty = true;

                    //EventManager.OnOrderCompleted.Invoke();
                    MachineController.Instance.EarnMoney(other.transform.GetComponentInParent<Customer>().machineID);
                    //MoneyManager.Instance.IncreaseMoney(_moneyAmount);
                    //ChefManager.Instance.ActiveChef().chefOrderController.TakeOrder();
                    //ChefOrderController.Instance.TakeOrder();
                    // ADD INCOME
                }
                else
                {
                    Debug.Log("yanlıs musteri");
                }
            }
        }
    }
}
