using TMPro;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    [SerializeField] private TextMeshProUGUI _moneyTextMesh;

    public float Money
    {
        get { return PlayerPrefs.GetFloat(PlayerPrefKeys.MoneyData, 50); }
        set { PlayerPrefs.SetFloat(PlayerPrefKeys.MoneyData, value); }
    }

    private void Start()
    {
        SetMoneyTextMesh();
    }

    public void IncreaseMoney(float value)
    {
        Money += value;

        SetMoneyTextMesh();
        EventManager.OnMoneyDataChange.Invoke();
    }

    public void DecreaseMoney(float value)
    {
        Money -= value;

        if (Money <= 0)
            Money = 0;

        SetMoneyTextMesh();
        EventManager.OnMoneyDataChange.Invoke();
    }

    private void SetMoneyTextMesh()
    {
        _moneyTextMesh.SetText(Money.ToString("0.0"));
    }
}
