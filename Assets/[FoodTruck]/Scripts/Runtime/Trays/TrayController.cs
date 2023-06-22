using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayController : Singleton<TrayController>
{
    [SerializeField] private Transform trayParent;
    [SerializeField] private Animator conveyor;
    [SerializeField] private List<Tray> trayList = new List<Tray>();

    
    public int lastActiveTray
    {
        get => PlayerPrefs.GetInt("LastActiveTray", 1);
        set => PlayerPrefs.SetInt("LastActiveTray", value);
    }

    public float conveyorSpeed
    {
        get => PlayerPrefs.GetFloat("ConveyorSpeed", 1);
        set => PlayerPrefs.SetFloat("ConveyorSpeed", value);
    }
    
    private void Start()
    {
        ActivateActiveTrays();
    }

    public void ActivateNewTray()
    {
        if(lastActiveTray >= trayParent.childCount)
            return;
        
        trayParent.GetChild(lastActiveTray).GetComponent<Tray>().ActivateTray();
        lastActiveTray += 1;
    }

    private void ActivateActiveTrays()
    {
        for (int i = 0; i < lastActiveTray; i++)
        {
            trayParent.GetChild(i).GetComponent<Tray>().ActivateTray();
        }
    }

    public void AddTrayToList(Tray tray)
    {
        trayList.Add(tray);
    }

    public void UpgradeSpeed()
    {
        conveyorSpeed += 0.1f;
        conveyorSpeed = Mathf.Clamp(conveyorSpeed, 1, 10);
        conveyor.speed = conveyorSpeed;
        Debug.Log("conveyor speed" + conveyor.speed);
    }
}
