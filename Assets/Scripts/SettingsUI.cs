using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sfxToggle;

    public void RefreshToggles()
    {
        _musicToggle.SetIsOnWithoutNotify(AudioManager.instance.IsMusicEnabled);
        _sfxToggle.SetIsOnWithoutNotify(AudioManager.instance.IsSFXEnabled);
    }
}