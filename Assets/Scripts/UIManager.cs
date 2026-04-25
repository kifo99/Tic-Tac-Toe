using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    public static UIManager instance;
    private GameObject _currentPanel;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are more than one GameManagers in this scene!");
        }
    }


    public void OpenPanel(GameObject panelToOpen)
    {

        if (_currentPanel != null)
        {
            Destroy(_currentPanel);
        }

        _currentPanel = Instantiate(panelToOpen, _canvas);
    }

    public void ClosePanel()
    {
        if (_currentPanel != null)
        {
            Destroy(_currentPanel);
            _currentPanel = null;
        }

    }
}
