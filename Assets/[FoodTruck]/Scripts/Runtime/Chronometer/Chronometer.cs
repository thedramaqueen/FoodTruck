using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronometer : Singleton<Chronometer>
{

    public float customerWaitingTime = 25;
    public float chefTakingOrderTime = 1;
    public float chefMovementTime = 1;
    //public float machineProductionTime => GetComponent<Machine>().productionTime;
}
