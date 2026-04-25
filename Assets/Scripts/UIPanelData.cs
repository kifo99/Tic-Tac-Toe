using UnityEngine;

[CreateAssetMenu(fileName = "UIPanelData", menuName = "UI/Data")]
public class UIPanelData : ScriptableObject
{
  [SerializeField] private GameObject _playPanel;
  [SerializeField] private GameObject _statsPanel;
  [SerializeField] private GameObject _settingsPanel;
  [SerializeField] private GameObject _exitPanel;


  public GameObject GetPlayPanel()
  {
    return _playPanel;
  }

  public GameObject GetStatsPanel()
  {
    return _statsPanel;
  }

  public GameObject GetSettingsPanel()
  {
    return _settingsPanel;
  }

  public GameObject GetExitPanel()
  {
    return _exitPanel;
  }
}
