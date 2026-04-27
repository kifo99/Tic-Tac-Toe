using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;
    [SerializeField] private XOTheme[] themes;
    [SerializeField] private string _defaultThemeName;
    private XOTheme _selectedTheme;
    private XOTheme _defaultTheme;


    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (_selectedTheme == null)
        {
            SetTheme(_defaultThemeName);
        }
    }

    public void SetTheme(string themeName)
    {
        for (int i = 0; i < themes.Length; i++)
        {
            if (themes[i].ThemeName == themeName)
            {
                _selectedTheme = themes[i];
            }

        }
    }

    public void SetToDefault()
    {
        SetTheme(_defaultThemeName);
    }

    public XOTheme GetCurrentTheme()
    {
        if (_selectedTheme != null)
        {
            return _selectedTheme;
        }

        return _defaultTheme;
    }
}
