using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private UIPanelData _uiData;

    public void RetryGame()
    {
        AudioManager.instance.PlayClick();
        GameManager.instance.IncreaseGameCounter();
        GameManager.instance.StartNewGame();
    }

    public void OnReturnToMenu()
    {
        AudioManager.instance.PlayClick();
        SceneManager.LoadScene("PlayScene");

    }


    public void OnPlayGame()
    {
        AudioManager.instance.PlayClick();

        UIManager.instance.OpenPanel(_uiData.GetPlayPanel());
    }

    public void OnStats()
    {
        AudioManager.instance.PlayClick();

        UIManager.instance.OpenPanel(_uiData.GetStatsPanel());

        _uiData.GetStatsPanel().GetComponentInChildren<StatsUI>().RefreshStats();
    }

    public void OnSettings()
    {
        AudioManager.instance.PlayClick();
        UIManager.instance.OpenPanel(_uiData.GetSettingsPanel());
    }

    public void OnExit()
    {
        AudioManager.instance.PlayClick();
        UIManager.instance.OpenPanel(_uiData.GetExitPanel());
    }

    public void OnStart()
    {
        AudioManager.instance.PlayClick();
        SceneManager.LoadScene("GameScene");
    }

    public void OnThemeSelection(string themeName)
    {
        AudioManager.instance.PlayClick();

        ThemeManager.instance.SetTheme(themeName);
    }
    public void OnBack()
    {
        AudioManager.instance.PlayClick();

        UIManager.instance.ClosePanel();
    }

    public void OnYes()
    {
        AudioManager.instance.PlayClick();
        Application.Quit();
    }

    public void OnMusicToggle(bool isOn)
    {
        Debug.Log("OnMusicToggle called: " + isOn);
        AudioManager.instance.SetMusic(isOn);
    }

    public void OnSFXToggle(bool isOn)
    {
        Debug.Log("OnSFXToggle called: " + isOn);
        AudioManager.instance.SetSFX(isOn);
    }
}
