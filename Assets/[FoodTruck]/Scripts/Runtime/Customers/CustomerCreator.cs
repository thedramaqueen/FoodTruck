using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerCreator : Singleton<CustomerCreator>
{
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private List<string> _productList;

    public Vector3 left, right, up, down;
    //[SerializeField] private List<Transform> _customerTargets;

    public int customerCount = 1;

    public int targetIndex = 0;
    //private int _productIndex = 0;

    public float timer;
    public float timeInterval;
    
    public int productIndex
    {
        get => PlayerPrefs.GetInt("ProductIndex", 1);
        set => PlayerPrefs.SetInt("ProductIndex", value);
    }

    private void Start()
    {
        CreateCustomer();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            CreateCustomer();
            timer = timeInterval;
        }
    }

    private Vector3 SetTargetPos(int value)
    {
        if (value >= 0 && value < 3)
        {
            return left;
        }
        else if (value >= 3 && value < 5)
        {
            return down;
        }
        else if (value >= 5 && value < 8)
        {
            return right;
        }
        else
        {
            return up;
        }
    }

    public void CreateCustomer()
    {
        if(CustomerTargetTransform.Instance.EmptyPointIndex() == -1)
            return;
        
        for (int i = 0; i < customerCount; i++)
        {
            GameObject customer = Instantiate(_customerPrefab, SetTargetPos(CustomerTargetTransform.Instance.EmptyPointIndex()), Quaternion.identity);
            customer.GetComponent<Customer>().destinationTarget =
                SetTargetPos(CustomerTargetTransform.Instance.EmptyPointIndex());
            customer.GetComponent<CustomerMovementController>().target =
                CustomerTargetTransform.Instance.FindEmptyTarget().position;
            targetIndex = CustomerTargetTransform.Instance.emptyIndex;
            
            customer.GetComponentInChildren<CustomerUISetter>().SetCanvasPosition(targetIndex);
            //customer.GetComponent<CustomerMovementController>().target = _customerTargets[Random.Range(0, _customerTargets.Count)].position;
            customer.GetComponent<Customer>().orderName = _productList[Random.Range(0, productIndex)];
            customer.GetComponent<Customer>().orderCount = Random.Range(1, 5);
            customer.GetComponent<Customer>().targetIndex = targetIndex;

            /*
            productIndex++;
            if (productIndex >= _productList.Count)
                productIndex = 0;
                */
        }
    }

    public void UpgradeProducts()
    {
        if(productIndex>_productList.Count)
            return;
        
        productIndex += 1;
    }
}
