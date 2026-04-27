using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sfxToggle;

    private void OnEnable()
    {
        _musicToggle.SetIsOnWithoutNotify(AudioManager.instance.IsMusicEnabled);
        _sfxToggle.SetIsOnWithoutNotify(AudioManager.instance.IsSFXEnabled);

        _musicToggle.onValueChanged.AddListener(OnMusicToggle);
        _sfxToggle.onValueChanged.AddListener(OnSFXToggle);
    }

    private void OnDisable()
    {
        _musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
        _sfxToggle.onValueChanged.RemoveListener(OnSFXToggle);
    }

    private void OnMusicToggle(bool isOn)
    {
        AudioManager.instance.SetMusic(isOn);
    }

    private void OnSFXToggle(bool isOn)
    {
        AudioManager.instance.SetSFX(isOn);
    }
}