using System;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefTakingOrderController : MonoBehaviour
{
    [SerializeField] private List<Machine> _machineList;
    [SerializeField] private Transform productParent;
    [SerializeField] private GameObject _loadingBarObject;
    [SerializeField] private Image _loadingCircleImage;

    private Chef _chef;

    private Chef chef => _chef == null ? _chef = GetComponent<Chef>() : _chef;

    private ChefAnimationController _chefAnimation;
    private ChefAnimationController ChefAnimation => _chefAnimation == null ? _chefAnimation = GetComponent<ChefAnimationController>() : _chefAnimation;

    private ChefOrderController _chefOrderController;
    private ChefOrderController ChefOrderController => _chefOrderController == null ? _chefOrderController = GetComponent<ChefOrderController>() : _chefOrderController;

    
    private Machine _machine;

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

    public void SetMachine(Machine machine)
    {
        _machine = machine;
    }

    public void StartTakingAction(float time, Vector3 position, Quaternion rotation)
    {
        transform.GetChild(0).DORotateQuaternion(rotation, 0.5f).SetEase(Ease.Linear);
        transform.DOMove(position, 1).SetEase(Ease.Linear).OnStart(() =>
        {
            ChefAnimation.ChangeAnimation(ChefAnimation.RUNNING_ANIM);
        }).OnComplete(() =>
        {
            ChefAnimation.ChangeAnimation(ChefAnimation.ORDER_ANIM);

            _loadingBarObject.SetActive(true);
            _loadingBarObject.transform.localScale = Vector3.zero;
            _loadingBarObject.transform.DOScale(new Vector3(6.473f, 6.473f, 6.473f), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _loadingCircleImage.DOFillAmount(1, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    ChefOrderController.customerList[0].OrderAction();
                    //ChefManager.Instance.ActiveChef().chefOrderController.customerList[0].OrderAction();
                    //ChefOrderController.Instance.customerList[0].OrderAction();

                    _loadingBarObject.transform.DOScale(Vector3.zero, 0.25f).SetDelay(0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        _loadingBarObject.SetActive(false);
                        _loadingCircleImage.fillAmount = 0;
                        // Next Action
                        ChefOrderController.TakingOrderAction();
                        //EventManager.OnTakingOrderCompleted.Invoke();
                    });
                });
            });
        });
    }

    public void MakeOrder(float time, Vector3 position, Quaternion rotation)
    {
        transform.GetChild(0).DORotateQuaternion(rotation, 0.5f).SetEase(Ease.Linear);
        transform.DOMove(position, 1).SetEase(Ease.Linear).OnStart(() =>
        {
            ChefAnimation.ChangeAnimation(ChefAnimation.RUNNING_ANIM);
        }).OnComplete(() =>
        {
            ChefAnimation.ChangeAnimation(ChefAnimation.ORDER_ANIM);
            chef.chefOrderController.customerList[0].OrderAction();
            //ChefManager.Instance.ActiveChef().chefOrderController.customerList[0].OrderAction();
            //ChefOrderController.Instance.customerList[0].OrderAction();

            _loadingBarObject.SetActive(true);
            _loadingBarObject.transform.localScale = Vector3.zero;
            _loadingBarObject.transform.DOScale(new Vector3(6.473f, 6.473f, 6.473f), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _loadingCircleImage.DOFillAmount(1, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _loadingBarObject.transform.DOScale(Vector3.zero, 0.25f).SetDelay(0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        _loadingBarObject.SetActive(false);
                        _loadingCircleImage.fillAmount = 0;

                        // Next Action
                        _machine.CreateOrder();
                        _chefOrderController.OrderCompletedAction();
                        //EventManager.OnMakeOrder.Invoke();
                        _machine.product.GoToOwner(productParent, Vector3.zero);
                        _machine.product.SetCustomer(chef.chefOrderController.customerList[0]);
                        chef.chefOrderController.customerList[0].currentProduct = _machine.product;
                        chef.currentProduct = GetComponentInChildren<Product>();
                        chef.isOccupied = true;
                        //Chef.Instance.currentProduct = GetComponentInChildren<Product>();
                        //Chef.Instance.isOccupied = true;
                    });
                });
            });
        });
    }
}
