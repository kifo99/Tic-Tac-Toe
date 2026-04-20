using UnityEngine;
using UnityEngine.UI;

public class GridSquareManager : MonoBehaviour
{
    [SerializeField]
    private Image _oImage;
    [SerializeField] private Image _xImage;

    private GridSquareState _currentState = GridSquareState.empty;

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
    }
}


public enum GridSquareState { empty, x, o };