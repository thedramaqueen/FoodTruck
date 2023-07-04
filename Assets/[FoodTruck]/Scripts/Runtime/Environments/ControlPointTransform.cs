using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointTransform : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Plate"))
      {
         Tray tray = other.GetComponent<Tray>();
         if(!tray.isEmpty)
            tray.currentOrderProduct.ControlCustomer();
      }
   }
}
