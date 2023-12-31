using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Chef : MonoBehaviour
{
    public int chefID;
    
    [FormerlySerializedAs("currentProduct")] public OrderProduct currentOrderProduct = null;
   
    public Transform sendOrderTransform;

    public ChefOrderController chefOrderController;
    public ChefTakingOrderController chefTakingOrderController;
    public ChefAnimationController chefAnimationController;
    
    public bool isOccupied;
    public bool isActive;
    public bool isAvailable = true;

    private void Start()
    {
        //ChefManager.Instance.AddChefToList(this);
        sendOrderTransform = SendOrderTransform.Instance.transform;
        isActive = true;
        isAvailable = true;
    }

    public void ActivateChef()
    {
        isActive = true;
        isAvailable = true;
    }

    public void StartTakingOrder(Customer customer)
    {
        isAvailable = false;
        chefOrderController.AddCustomer(customer);
    }

    public void CompleteOrder()
    {
        currentOrderProduct = null;
        isOccupied = false;
        //isAvailable = true;
    }
}
