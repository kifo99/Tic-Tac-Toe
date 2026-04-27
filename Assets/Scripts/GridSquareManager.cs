using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSquareManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _oImage;
    [SerializeField] private Image _xImage;

    private XOTheme _theme;
    private GridSquareState _currentState = GridSquareState.empty;
    private int _squareId;

    private void Start()
    {
        _oImage.enabled = false;
        _xImage.enabled = false;

        ApplyTheme();
    }

    private void ApplyTheme()
    {
        if (ThemeManager.instance == null) return;

        _theme = ThemeManager.instance.GetCurrentTheme();

        if (_theme != null)
        {
            _xImage.sprite = _theme.xSprite;
            _oImage.sprite = _theme.oSprite;
        }
    }

    public void RefreshTheme()
    {
        if (ThemeManager.instance == null) return;

        _theme = ThemeManager.instance.GetCurrentTheme();

        if (_theme == null) return;

        _xImage.sprite = _theme.xSprite;
        _oImage.sprite = _theme.oSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.GridSquareClicked(_squareId);
    }

    public GridSquareState GetSquareState()
    {
        return _currentState;
    }

    public void SetSquare(GridSquareState _newState)
    {
        if (_newState == GridSquareState.empty)
        {
            _oImage.enabled = false;
            _xImage.enabled = false;
        }
        else if (_newState == GridSquareState.x)
        {
            _oImage.enabled = false;
            _xImage.enabled = true;
        }
        else if (_newState == GridSquareState.o)
        {
            _oImage.enabled = true;
            _xImage.enabled = false;
        }

        _currentState = _newState;
    }

    public void SetSquareId(int id)
    {
        _squareId = id;
    }
}

public enum GridSquareState { empty, x, o };