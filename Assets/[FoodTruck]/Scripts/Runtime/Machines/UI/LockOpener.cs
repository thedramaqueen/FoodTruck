using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LockOpener : MonoBehaviour
{
   
    public GameObject unlockPanel;
    public GameObject lockPanel;
    public Button button;
    
    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        unlockPanel.SetActive(true);
        lockPanel.SetActive(false);
    }
}
