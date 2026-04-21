using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSquareManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _oImage;
    [SerializeField] private Image _xImage;

    private GridSquareState _currentState = GridSquareState.empty;

    private int _squareId;

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