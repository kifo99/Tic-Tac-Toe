using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _canvasTag = "UICanvas";

    public static UIManager instance;
    private GameObject _currentPanel;
    private Transform _canvas;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        FindCanvas();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentPanel = null;
        FindCanvas();
    }

    void FindCanvas()
    {
        GameObject canvasObj = GameObject.FindWithTag(_canvasTag);
        if (canvasObj != null)
        {
            _canvas = canvasObj.transform;
        }
        else
        {
            Debug.LogWarning($"UIManager: No GameObject with tag '{_canvasTag}' found in scene.");
        }
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        if (_canvas == null)
        {
            Debug.LogError("UIManager: _canvas is null. Make sure your Canvas has the correct tag.");
            return;
        }

        if (_currentPanel != null)
            Destroy(_currentPanel);

        _currentPanel = Instantiate(panelToOpen, _canvas);
        AudioManager.instance.PlayPopup();
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