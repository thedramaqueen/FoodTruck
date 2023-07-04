using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tray : MonoBehaviour
{
    [FormerlySerializedAs("currentProduct")] public OrderProduct currentOrderProduct;
    public bool isActive = false;
    public bool isEmpty = true;
    public Transform productPointTransform;

    public void ActivateTray()
    {
        isActive = true;
        gameObject.SetActive(true);
        TrayController.Instance.AddTrayToList(this);
    }
}
