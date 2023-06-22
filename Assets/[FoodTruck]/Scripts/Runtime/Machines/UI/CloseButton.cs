using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{

    public GameObject upgradePanel;
    public GameObject panelOpener;
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
        upgradePanel.SetActive(false);
        panelOpener.SetActive(true);
    }
}
