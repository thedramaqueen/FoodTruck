using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioClip> audioClips;

    private AudioSource _audioSource;
    private AudioSource AudioSource => _audioSource == null ? _audioSource = GetComponent<AudioSource>() : _audioSource;

    public float randomPercent = 5;

    public bool isSoundActive;
    private int _stateCount;
    private int _pitchCounter = 0;

    private void Start()
    {
        GetStateData();
    }

    public void PlaySound(int index)
    {
        if (isSoundActive && SettingManager.Instance.soundStateCount == 0)
        {
            AudioSource.clip = audioClips[index];
            AudioSource.pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);

            AudioSource.PlayOneShot(AudioSource.clip);

            _pitchCounter++;
            if (_pitchCounter > 4)
                AudioSource.pitch = 1;
        }
    }

    public void PlayNormalSound(int index)
    {
        if (isSoundActive && SettingManager.Instance.soundStateCount == 0)
        {
            AudioSource.pitch = 1;
            AudioSource.clip = audioClips[index];
            AudioSource.PlayOneShot(AudioSource.clip);
        }
    }

    public void ChangeSoundState()
    {
        isSoundActive = !isSoundActive;
        EventManager.OnSoundStateChange.Invoke();
    }

    public void SoundStatePlayerPrefs(int value)
    {
        _stateCount = value;
        PlayerPrefs.SetInt(PlayerPrefKeys.SoundState, _stateCount);
    }

    private void GetStateData()
    {
        if (PlayerPrefs.HasKey(PlayerPrefKeys.SoundState))
            _stateCount = PlayerPrefs.GetInt(PlayerPrefKeys.SoundState);
        else
            _stateCount = 0;

        if (_stateCount == 0)
            isSoundActive = true;
        else
            isSoundActive = false;
    }
}