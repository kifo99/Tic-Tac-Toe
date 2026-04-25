using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Animator _animatorHorizontalLines;
    [SerializeField] private Animator _animatorVerticalLines;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private string _stateName;

    [SerializeField] private float _spawnDelay = 0.4f;
    [SerializeField] private float _resetDelay = 1.5f;


    void Start()
    {
        _animatorHorizontalLines.Play(_stateName, 0, 0f);
        _animatorVerticalLines.Play(_stateName, 0, 0f);
        StartCoroutine(XOBackgroundAnimation());
    }

    IEnumerator XOBackgroundAnimation()
    {
        while (true)
        {
            _gridManager.ResetGrid();

            GridSquareState currentTurn = (Random.value > 0.5f)
                ? GridSquareState.x
                : GridSquareState.o;

            while (!_gridManager.CheckIfGridFull())
            {
                int randomIndex = Random.Range(0, 9);

                if (_gridManager.GetSpecificSquareState(randomIndex) == GridSquareState.empty)
                {
                    _gridManager.SetSpecificSquare(currentTurn, randomIndex);

                    yield return new WaitForSeconds(_spawnDelay);

                    if (TicTacToeUtils.instance.CheckForWin(currentTurn, _gridManager))
                    {
                        yield return new WaitForSeconds(1f);
                        break;
                    }

                    currentTurn = (currentTurn == GridSquareState.x)
                        ? GridSquareState.o
                        : GridSquareState.x;
                }
            }

            yield return new WaitForSeconds(_resetDelay);
        }
    }

    public void StartPopup()
    {

    }
}
