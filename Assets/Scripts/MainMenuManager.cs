using System.Collections;
using UnityEngine;

public class GridAnimationManager : MonoBehaviour
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

            // Fill grid randomly
            while (!_gridManager.CheckIfGridFull())
            {
                int randomIndex = Random.Range(0, 9);

                if (_gridManager.GetSpecificSquareState(randomIndex) == GridSquareState.empty)
                {
                    GridSquareState randomState =
                        (Random.value > 0.5f) ? GridSquareState.x : GridSquareState.o;

                    _gridManager.SetSpecificSquare(randomState, randomIndex);

                    yield return new WaitForSeconds(_spawnDelay);
                }
            }

            // Wait before clearing
            yield return new WaitForSeconds(_resetDelay);
        }
    }
}
