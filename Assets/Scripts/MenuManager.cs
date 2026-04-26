using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private Animator _animatorHorizontalLines;
    [SerializeField] private Animator _animatorVerticalLines;
    [SerializeField] private string _stateName;

    [Header("Timing")]
    [SerializeField] private float _spawnDelay = 0.4f;
    [SerializeField] private float _resetDelay = 1.5f;

    [Header("Grid Visuals (9 UI Squares)")]
    [SerializeField] private GridSquareManager[] visualGrid;

    private GridSquareState[] fakeGrid = new GridSquareState[9];
    private Coroutine _routine;
    private bool _isRunning;

    private static readonly int[,] winPatterns = new int[,]
    {
        {0,1,2},
        {3,4,5},
        {6,7,8},
        {0,3,6},
        {1,4,7},
        {2,5,8},
        {0,4,8},
        {2,4,6}
    };

    void Start()
    {
        StartAnimation();
    }

    void OnDisable()
    {
        StopAnimation();
    }

    void OnEnable()
    {
        if (_routine == null && _isRunning == false && visualGrid != null)
            StartCoroutine(DelayedStart());
    }

    void StartAnimation()
    {
        StopAnimation();

        if (_animatorHorizontalLines != null)
            _animatorHorizontalLines.Play(_stateName, 0, 0f);

        if (_animatorVerticalLines != null)
            _animatorVerticalLines.Play(_stateName, 0, 0f);

        _isRunning = true;
        _routine = StartCoroutine(XOBackgroundAnimation());
    }

    void StopAnimation()
    {
        _isRunning = false;

        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
    }

    IEnumerator DelayedStart()
    {
        yield return null;
        yield return null;

        StartAnimation();
    }

    IEnumerator XOBackgroundAnimation()
    {
        while (_isRunning)
        {
            ResetBoard();

            GridSquareState currentTurn =
                (Random.value > 0.5f) ? GridSquareState.x : GridSquareState.o;

            int moves = 0;

            while (moves < 9 && _isRunning)
            {
                int randomIndex = Random.Range(0, 9);

                if (fakeGrid[randomIndex] == GridSquareState.empty)
                {
                    fakeGrid[randomIndex] = currentTurn;
                    moves++;

                    if (visualGrid != null &&
                        randomIndex >= 0 &&
                        randomIndex < visualGrid.Length &&
                        visualGrid[randomIndex] != null)
                    {
                        visualGrid[randomIndex].SetSquare(currentTurn);
                    }

                    yield return new WaitForSeconds(_spawnDelay);

                    if (CheckWin(currentTurn))
                        break;

                    currentTurn = (currentTurn == GridSquareState.x)
                        ? GridSquareState.o
                        : GridSquareState.x;
                }
            }

            yield return new WaitForSeconds(_resetDelay);
        }
    }

    void ResetBoard()
    {
        for (int i = 0; i < fakeGrid.Length; i++)
        {
            fakeGrid[i] = GridSquareState.empty;

            if (visualGrid != null &&
                i < visualGrid.Length &&
                visualGrid[i] != null)
            {
                visualGrid[i].RefreshTheme();
                visualGrid[i].SetSquare(GridSquareState.empty);
            }
        }
    }

    bool CheckWin(GridSquareState state)
    {
        for (int i = 0; i < 8; i++)
        {
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];

            if (fakeGrid[a] == state &&
                fakeGrid[b] == state &&
                fakeGrid[c] == state)
            {
                return true;
            }
        }

        return false;
    }
}