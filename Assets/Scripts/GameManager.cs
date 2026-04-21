using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  [SerializeField] private GridManager _gridManager;
  private GridSquareState _playerOneSquareState;
  private GridSquareState _playerTwoSquareState;
  private bool _awaitingInput = false;
  private Turn _currentTurn;
  private GameResults _currentGameStates;
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
      Debug.LogError("There are more than one GameManagers in this scene!");
    }
    StartNewGame();
  }
  private void StartNewGame()
  {
    // Setting state of a game
    _currentGameStates = GameResults.ongoing;
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

    _awaitingInput = true;
  }


  private void ProcessTurn(Turn turn, int selectedSquare)
  {
    _awaitingInput = false;
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

    bool gameEnded = CheckIfGameEnded();
    if (!gameEnded)
    {
      ChangeTurn();
      _awaitingInput = true;
    }
  }

  private bool CheckIfGameEnded()
  {

    if (CheckForWin(_playerOneSquareState))
    {
      _currentGameStates = GameResults.playerOneWin;
      Debug.Log("Player1 is a winner!");
      return true;
    }

    if (CheckForWin(_playerTwoSquareState))
    {
      _currentGameStates = GameResults.playerTwoWin;
      Debug.Log("Player2 is a winner!");
      return true;
    }
    bool gridFull = _gridManager.CheckIfGridFull();

    if (gridFull)
    {
      _currentGameStates = GameResults.draw;
      Debug.Log("Draw!");
      return true;
    }

    return false;
  }

  private bool CheckForWin(GridSquareState squareState)
  {
    foreach (var pattern in _winPatterns)
    {
      if (_gridManager.GetSpecificSquareState(pattern[0]) == squareState && _gridManager.GetSpecificSquareState(pattern[1]) == squareState && _gridManager.GetSpecificSquareState(pattern[2]) == squareState)
      {
        return true;
      }
    }
    return false;
  }
  public void ChangeTurn()
  {
    if (_currentTurn == Turn.playerOne)
    {
      _currentTurn = Turn.playerTwo;
    }
    else
    {
      _currentTurn = Turn.playerOne;
    }
  }

  public void GridSquareClicked(int clickedSquare)
  {
    if (_awaitingInput == false || _currentGameStates != GameResults.ongoing)
    {
      return;
    }
    if (_gridManager.GetSpecificSquareState(clickedSquare) != GridSquareState.empty) { return; }

    ProcessTurn(_currentTurn, clickedSquare);

  }
}


public enum Turn { playerOne, playerTwo }
public enum GameResults { ongoing, draw, playerOneWin, playerTwoWin }