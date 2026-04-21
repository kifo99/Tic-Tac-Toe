using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  [SerializeField] private GridManager _gridManager;
  private GridSquareState _playerOneSquareState;
  private GridSquareState _playerTwoSquareState;

  private Turn _currentTurn;

  public void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Debug.LogError("There are more than one GameManagers in this scene!");
    }
    StartNewGame();
  }
  private void StartNewGame()
  {
    // Resetting the grid
    _gridManager.ResetGrid();

    // Randomly deciding who has first turn
    int firstTurn = Random.Range(0, 2);
    _currentTurn = (Turn)firstTurn;

    // Assigning state of grid square(cell) to player
    if (firstTurn == 0)
    {
      _playerOneSquareState = GridSquareState.x;
      _playerTwoSquareState = GridSquareState.o;
    }
    else
    {
      _playerOneSquareState = GridSquareState.o;
      _playerTwoSquareState = GridSquareState.x;
    }
  }


  private void ProcessTurn(Turn turn, int selectedSquare)
  {
    GridSquareState state = GridSquareState.empty;
    if (turn == Turn.playerOne)
    {
      state = _playerOneSquareState;
    }
    else
    {
      state = _playerTwoSquareState;
    }
    _gridManager.SetSpecificSquare(state, selectedSquare);
  }

  public void GridSquareClicked(int clickedSquare)
  {
    Debug.Log("Square Clicked: " + clickedSquare);
  }
}


public enum Turn { playerOne, playerTwo }