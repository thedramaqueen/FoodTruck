using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    //[SerializeField] private Chronometer chronometer;
    public string orderName;
    public int orderCount;
    public int targetIndex;
    public int chefID;
    public int machineID;
    public bool isOrdered = false;
    public bool isLeft;

    public Vector3 destinationTarget;
    public Product currentProduct;
    public CustomerUISetter customerUISetter;
    
    [SerializeField] private TMP_Text countText;
    [SerializeField] private GameObject _orderObject;
    [SerializeField] private Image _timerImage;
    [SerializeField] private List<GameObject> orderObjects = new List<GameObject>();

    private string _timerTweenID;

    private CustomerAnimationController _customerAnimation;
    private CustomerAnimationController CustomerAnimation => _customerAnimation == null ? _customerAnimation = GetComponent<CustomerAnimationController>() : _customerAnimation;

    private CustomerMovementController _customerMovement;
    private CustomerMovementController CustomerMovement => _customerMovement == null ? _customerMovement = GetComponent<CustomerMovementController>() : _customerMovement;

    private void Awake()
    {
        _timerTweenID = GetInstanceID() + "timerTween";
    }

    public void OrderAction()
    {
        _orderObject.SetActive(true);
        SetOrderObject();
        _orderObject.transform.localScale = Vector3.zero;
        _orderObject.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            CustomerAnimation.ChangeAnimation(CustomerAnimation.ORDER_ANIM);

            _timerImage.DOFillAmount(0, Chronometer.Instance.customerWaitingTime*orderCount).SetEase(Ease.Linear).SetId(_timerTweenID).OnComplete(() =>
            {
                TakeAction();
                isLeft = true;
                currentProduct.ControlCustomer();
                Debug.Log("musteri kacti...");
            });
        });
    }

    public void TakeAction()
    {
        DOTween.Kill(_timerTweenID);
        _orderObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _orderObject.SetActive(false);
            _timerImage.fillAmount = 0;

            CustomerMovement.isEmpty = true;
            CustomerMovement.target = destinationTarget;
            CustomerMovement.StartMovement();
            CustomerAnimation.ChangeAnimation(CustomerAnimation.CARRYING_ANIM);
            CustomerCreator.Instance.CreateCustomer();
            Destroy(gameObject, 2f);
        });
        
        CustomerTargetTransform.Instance.SetTargetAvailable(targetIndex);
    }

    public void SetOrderText()
    {
        orderCount -= 1;
        countText.SetText(orderCount.ToString());
        if (orderCount <= 0)
        {
            TakeAction();
        }
    }

    private void SetOrderObject()
    {
        countText.SetText(orderCount.ToString());
        
        switch (orderName)
        {
            case "pizza":
                machineID = 0;
                orderObjects[0].SetActive(true);
                break;
            case "coffe":
                machineID = 1;
                orderObjects[1].SetActive(true);
                break;
            default:
                machineID = 2;
                orderObjects[2].SetActive(true);
                break;
        }
    }
}
