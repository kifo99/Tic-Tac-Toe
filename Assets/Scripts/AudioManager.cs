using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Background Music")]
    [SerializeField] private AudioSource _backgroundMusic;

    [Header("SFX")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _buttonClickClip;
    [SerializeField] private AudioClip _placementClickClip;
    [SerializeField] private AudioClip _popupClip;

    private const string MusicPrefKey = "MusicEnabled";
    private const string SfxPrefKey = "SFXEnabled";

    public bool IsMusicEnabled { get; private set; }
    public bool IsSFXEnabled { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        IsMusicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        IsSFXEnabled = PlayerPrefs.GetInt(SfxPrefKey, 1) == 1;

        ApplyMusicState();
        ApplySFXState();
    }


    public void SetMusic(bool enabled)
    {
        IsMusicEnabled = enabled;
        PlayerPrefs.SetInt(MusicPrefKey, enabled ? 1 : 0);
        ApplyMusicState();
    }

    public void SetSFX(bool enabled)
    {
        IsSFXEnabled = enabled;
        PlayerPrefs.SetInt(SfxPrefKey, enabled ? 1 : 0);
        ApplySFXState();
    }


    public void PlayClick()
    {
        if (IsSFXEnabled) _sfxSource.PlayOneShot(_buttonClickClip);
    }

    public void PlacementClick()
    {
        if (IsSFXEnabled) _sfxSource.PlayOneShot(_placementClickClip);
    }

    public void PlayPopup()
    {
        if (IsSFXEnabled) _sfxSource.PlayOneShot(_popupClip);
    }


    private void ApplyMusicState()
    {
        if (_backgroundMusic == null) return;

        if (IsMusicEnabled && !_backgroundMusic.isPlaying)
            _backgroundMusic.Play();
        else if (!IsMusicEnabled)
            _backgroundMusic.Pause();
    }

    private void ApplySFXState()
    {
        if (_sfxSource != null)
            _sfxSource.mute = !IsSFXEnabled;
    }
}