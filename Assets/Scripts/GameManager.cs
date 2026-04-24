using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  [SerializeField] private GridManager _gridManager;
  [SerializeField] private Animator _verticalAnimator;
  [SerializeField] private Animator _horizontalAnimator;
  [SerializeField] private GameTimer _gameTimer;
  [SerializeField] private TextMeshProUGUI _playerOneMoveText;
  [SerializeField] private TextMeshProUGUI _playerTwoMoveText;
  [SerializeField] private TextMeshProUGUI _gameResultText;
  [SerializeField] private GameObject _gameOverPanel;
  private GridSquareState _playerOneSquareState;
  private GridSquareState _playerTwoSquareState;
  private bool _awaitingInput = false;
  private Turn _currentTurn;
  private GameResults _currentGameStates;
  private int _gameCounter = 0;

  private int _playerOneMove = 0;
  private int _playerTwoMove = 0;
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
    // Drawing grid lines
    _verticalAnimator.Play("Draw");
    _horizontalAnimator.Play("Draw");
    StartNewGame();
  }

  public void Update()
  {
    _gameTimer.UpdateTimerUi();
  }
  public void StartNewGame()
  {
    // Hiding game over popup
    _gameOverPanel.SetActive(false);

    // Setting state of a game
    _currentGameStates = GameResults.ongoing;

    // Resetting the grid
    _gridManager.ResetGrid();

    // Starting game timer
    _gameTimer.StartTimer();

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

  public void IncreaseGameCounter()
  {
    _gameCounter++;
  }

  public int GetGameCounter()
  {
    return _gameCounter;
  }
  private void ProcessTurn(Turn turn, int selectedSquare)
  {
    _awaitingInput = false;
    GridSquareState state = GridSquareState.empty;
    if (turn == Turn.playerOne)
    {
      state = _playerOneSquareState;
      _playerOneMove++;
      UpdateMoveUI(_playerOneMoveText, _playerOneMove, turn);
    }
    else
    {
      state = _playerTwoSquareState;
      _playerTwoMove++;
      UpdateMoveUI(_playerTwoMoveText, _playerTwoMove, turn);
    }
    _gridManager.SetSpecificSquare(state, selectedSquare);

    bool gameEnded = CheckIfGameEnded();
    if (!gameEnded)
    {
      ChangeTurn();
      _awaitingInput = true;
    }
  }

  private void UpdateMoveUI(TextMeshProUGUI playerText, int playerMove, Turn player)
  {
    string playerLabel = (player == Turn.playerOne) ? "P1" : "P2";
    playerText.text = $"{playerLabel}: {playerMove}";
  }



  private bool CheckIfGameEnded()
  {
    int minutes = Mathf.FloorToInt(_gameTimer.GetTime() / 60f);
    int seconds = Mathf.FloorToInt(_gameTimer.GetTime() % 60f);

    if (CheckForWin(_playerOneSquareState))
    {

      _currentGameStates = GameResults.playerOneWin;
      GameOverPopup("Player 1 win!");
      _gameTimer.StopTimer();
      return true;
    }

    if (CheckForWin(_playerTwoSquareState))
    {
      _currentGameStates = GameResults.playerTwoWin;
      GameOverPopup("Player 2 win!");
      _gameTimer.StopTimer();
      return true;
    }
    bool gridFull = _gridManager.CheckIfGridFull();

    if (gridFull)
    {
      _currentGameStates = GameResults.draw;
      GameOverPopup("Draw!");
      _gameTimer.StopTimer();
      return true;
    }

    return false;
  }

  private void GameOverPopup(string popupText)
  {
    int minutes = Mathf.FloorToInt(_gameTimer.GetTime() / 60f);
    int seconds = Mathf.FloorToInt(_gameTimer.GetTime() % 60f);

    _gameOverPanel.SetActive(true);
    _gameResultText.text = $"{popupText} Timer: {minutes}:{seconds}";
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