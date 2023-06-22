using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public Product currentProduct;
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
