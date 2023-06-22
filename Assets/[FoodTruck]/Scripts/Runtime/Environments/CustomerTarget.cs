using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTarget : MonoBehaviour
{

    public bool isActive = true;
    public bool isFull = false;

    public void SetAvailable(bool state)
    {
        isFull = state;
    }
}
