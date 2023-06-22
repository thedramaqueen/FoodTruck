using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTargetTransform : Singleton<CustomerTargetTransform>
{

    public int emptyIndex = 0;
    public List<CustomerTarget> customerTargets = new List<CustomerTarget>();
    
    
    public Transform FindEmptyTarget()
    {
        emptyIndex = customerTargets.FindIndex(0, x => x.isActive && x.isFull == false);
        customerTargets[emptyIndex].SetAvailable(true);
        return customerTargets[emptyIndex].transform;
    }

    public int EmptyPointIndex()
    {
        var point = customerTargets.FindIndex(0, x => x.isActive && x.isFull == false);
        return point;
    }

    public void SetTargetAvailable(int targetIndex)
    {
        customerTargets[targetIndex].SetAvailable(false);
    }

}
