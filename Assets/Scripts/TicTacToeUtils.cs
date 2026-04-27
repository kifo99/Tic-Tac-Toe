using UnityEngine;

public class TicTacToeUtils : MonoBehaviour
{
    public static TicTacToeUtils instance;

    private readonly int[][] _winPatterns = new int[][] {
        new int[] {0, 1, 2},
        new int[] {3, 4, 5},
        new int[] {6, 7, 8},
        new int[] {0, 3, 6},
        new int[] {1, 4, 7},
        new int[] {2, 5, 8},
        new int[] {0, 4, 8},
        new int[] {2, 4, 6}
    };

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are more than one instances in this scene!");
        }
    }

    public bool CheckForWin(GridSquareState squareState, GridManager gridManager)
    {
        foreach (var pattern in _winPatterns)
        {
            if (gridManager.GetSpecificSquareState(pattern[0]) == squareState && gridManager.GetSpecificSquareState(pattern[1]) == squareState && gridManager.GetSpecificSquareState(pattern[2]) == squareState)
            {
                return true;
            }
        }
        return false;
    }

}
