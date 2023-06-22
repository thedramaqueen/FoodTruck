using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefOrderController : MonoBehaviour
{
    [SerializeField] private List<Machine> _machineList;

    public List<Customer> customerList;
    public int orderCount;
    
    [SerializeField] private Transform orderPoint;
    //[SerializeField] private Chronometer chronometer;
    private ChefTakingOrderController _chefTakingOrder;
    private ChefTakingOrderController ChefTakingOrder => _chefTakingOrder == null ? _chefTakingOrder = GetComponent<ChefTakingOrderController>() : _chefTakingOrder;

    private ChefAnimationController _chefAnimation;
    private ChefAnimationController ChefAnimation => _chefAnimation == null ? _chefAnimation = GetComponent<ChefAnimationController>() : _chefAnimation;

    private Chef _chef;

    private Chef chef => _chef == null ? _chef = GetComponent<Chef>() : _chef;
    
    private float _moneyAmount;
    private Machine _machine;
    
    /*
    private void OnEnable()
    {
        EventManager.OnNewOrder.AddListener(NewOrderAction);
        EventManager.OnTakingOrderCompleted.AddListener(TakingOrderAction);
        EventManager.OnMakeOrder.AddListener(OrderCompletedAction);
        EventManager.OnSendOrder.AddListener(ControlOrderCount);
    }

    private void OnDisable()
    {
        EventManager.OnNewOrder.RemoveListener(NewOrderAction);
        EventManager.OnTakingOrderCompleted.RemoveListener(TakingOrderAction);
        EventManager.OnMakeOrder.RemoveListener(OrderCompletedAction);
        EventManager.OnSendOrder.RemoveListener(ControlOrderCount);
    }*/
    
    private void Start()
    {
        AddMachines();
    }

    private void AddMachines()
    {
        for (int i = 0; i < MachineController.Instance._activeMachines.Count; i++)
        {
            _machineList.Add(MachineController.Instance._activeMachines[i]);
        }
        
    }

    private Transform FindDirection()
    {
        if (customerList[0].customerUISetter.isLeft)
        {
            orderPoint = ChefOrderTransform.Instance.left.GetChild(chef.chefID);
        }
        else if (customerList[0].customerUISetter.isRight)
        {
            orderPoint = ChefOrderTransform.Instance.right.GetChild(chef.chefID);
        }
        else if (customerList[0].customerUISetter.isUp)
        {
            orderPoint = ChefOrderTransform.Instance.up.GetChild(chef.chefID);
        }
        else if (customerList[0].customerUISetter.isDown)
        {
            orderPoint = ChefOrderTransform.Instance.down.GetChild(chef.chefID);
        }

        return orderPoint;
    }

    public void NewOrderAction()
    {
        ChefTakingOrder.StartTakingAction(Chronometer.Instance.chefTakingOrderTime, FindDirection().position, customerList[0].transform.rotation);
    }

    public void TakingOrderAction()
    {
        if(orderCount <= 0)
            return;
        
        customerList[0].isOrdered = true;
        ChefManager.Instance.RemoveFromWaitingList(customerList[0]);
        
        switch (customerList[0].orderName)
        {
            case "pizza":
                _machine = _machineList[0];
                _chefTakingOrder.SetMachine(_machine);
                ChefTakingOrder.MakeOrder(_machine.productionTime, _machine.chefTransform.GetChild(chef.chefID).position, _machine.transform.rotation);
                _moneyAmount = _machine.moneyAmount;
                break;
            case "coffe":
                _machine = _machineList[1];
                _chefTakingOrder.SetMachine(_machine);
                ChefTakingOrder.MakeOrder(_machine.productionTime*2, _machine.chefTransform.GetChild(chef.chefID).position, _machine.transform.rotation);
                _moneyAmount = _machine.moneyAmount;
                break;
            case "chicken":
                _machine = _machineList[2];
                _chefTakingOrder.SetMachine(_machine);
                ChefTakingOrder.MakeOrder(_machine.productionTime*3, _machine.chefTransform.GetChild(chef.chefID).position, _machine.transform.rotation);
                _moneyAmount = _machine.moneyAmount;
                break;
        }
    }

    public void OrderCompletedAction()
    {
        transform.GetChild(0).DORotateQuaternion(GetComponent<Chef>().sendOrderTransform.GetChild(chef.chefID).rotation, 0.5f).SetEase(Ease.Linear);
        transform.DOMove(GetComponent<Chef>().sendOrderTransform.GetChild(chef.chefID).position, Chronometer.Instance.chefMovementTime).OnStart(() =>
        {
            ChefAnimation.ChangeAnimation(ChefAnimation.CARRYING_ANIM);

        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            //SendOrderTransform.Instance.isOccupied = true;
            SendOrderTransform.Instance.AddChefToList(chef);
            //SendOrderTransform.Instance.chef = chef;
            ChefAnimation.ChangeAnimation(ChefAnimation.IDLECARRYING_ANIM);
            Debug.Log("siparis tamamlandi!");
            
        });
    }

    public void AddCustomer(Customer customer)
    {
        if (!customerList.Contains(customer))
        {
            customerList.Add(customer);
            orderCount = customer.orderCount;
            ChefManager.Instance.RemoveFromWaitingList(customer);
            NewOrderAction();
            //EventManager.OnNewOrder.Invoke();
        }
    }

    public void RemoveCustomer(Customer customer)
    {
        if (orderCount <= 0)
        {
            if (customerList.Contains(customer))
            {
                customerList.Remove(customer);
            }
        }
        
    }

    public void ControlOrderCount()
    {
        if (customerList[0] == null || customerList[0].isLeft)
        {
            customerList.RemoveAt(0);
            transform.GetComponent<Chef>().isAvailable = true;
            ChefManager.Instance.ControlWaitingList();
            return;
        }
        
        if (orderCount >= 1)
        {
            orderCount -= 1;
            
            if (orderCount == 0)
            {
                RemoveCustomer(customerList[0]);
                transform.GetComponent<Chef>().isAvailable = true;
                ChefManager.Instance.ControlWaitingList();
            }
            else
            {
                TakingOrderAction();
            }
        }
    }
}
