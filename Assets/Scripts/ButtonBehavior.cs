using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private UIPanelData _uiData;
    public void RetryGame()
    {
        GameManager.instance.IncreaseGameCounter();
        GameManager.instance.StartNewGame();
    }

    public void Exit()
    {
        SceneManager.LoadScene("PlayScene");
    }


    public void OnPlayGame()
    {
        Debug.Log(_uiData.GetPlayPanel());
        UIManager.instance.OpenPanel(_uiData.GetPlayPanel());
    }

    public void OnStats()
    {
        UIManager.instance.OpenPanel(_uiData.GetStatsPanel());

        _uiData.GetStatsPanel().GetComponentInChildren<StatsUI>().RefreshStats();
    }

    public void OnSettings()
    {
        UIManager.instance.OpenPanel(_uiData.GetSettingsPanel());
    }

    public void OnExit()
    {
        UIManager.instance.OpenPanel(_uiData.GetExitPanel());
    }

    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnThemeSelection(string themeName)
    {
        ThemeManager.instance.SetTheme(themeName);
    }
    public void OnBack()
    {
        UIManager.instance.ClosePanel();
    }
}
