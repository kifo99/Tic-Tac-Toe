using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSquareManager[] _grid;

    public void Awake()
    {
        ResetGrid();
    }
    public void ResetGrid()
    {
        foreach (GridSquareManager square in _grid)
        {
            square.SetSquare(GridSquareState.empty);
        }
    }
}
