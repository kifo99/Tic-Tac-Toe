using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSquareManager[] _grid;


    public void ResetGrid()
    {
        for (int i = 0; i < _grid.Length; i++)
        {
            _grid[i].SetSquare(GridSquareState.empty);
            _grid[i].SetSquareId(i);
        }
    }

    public void SetSpecificSquare(GridSquareState _gridSquareState, int _square)
    {
        _grid[_square].SetSquare(_gridSquareState);
    }

    public GridSquareState GetSpecificSquareState(int squareId)
    {
        return _grid[squareId].GetSquareState();
    }

    public bool CheckIfGridFull()
    {
        foreach (GridSquareManager square in _grid)
        {
            if (square.GetSquareState() == GridSquareState.empty)
            {
                return false;
            }
        }

        return true;
    }
}

