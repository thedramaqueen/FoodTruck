using UnityEngine;
using UnityEngine.Events;

public class SettingManager : Singleton<SettingManager>
{
    public int soundStateCount { get { return PlayerPrefs.GetInt(PlayerPrefKeys.SoundStateCount, 0); } set { PlayerPrefs.SetInt(PlayerPrefKeys.SoundStateCount, value); } }
    public int vibrationStateCount { get { return PlayerPrefs.GetInt(PlayerPrefKeys.VibrationStateCount, 0); } set { PlayerPrefs.SetInt(PlayerPrefKeys.VibrationStateCount, value); } }

    public static UnityEvent OnSoundStateChanged = new UnityEvent();
    public static UnityEvent OnVibrationStateChanged = new UnityEvent();

    private void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.SetQualityLevel(2);
    }

    public void ChangeSoundState()
    {
        if (soundStateCount == 0)
            soundStateCount = 1;
        else if (soundStateCount == 1)
            soundStateCount = 0;

        OnSoundStateChanged.Invoke();
    }

    public void ChangeVibrationState()
    {
        if (vibrationStateCount == 0)
            vibrationStateCount = 1;
        else if (vibrationStateCount == 1)
            vibrationStateCount = 0;

        OnVibrationStateChanged.Invoke();
    }
}
