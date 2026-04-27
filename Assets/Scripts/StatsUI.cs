using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalGamesText;
    [SerializeField] private TextMeshProUGUI _playerOneWinsText;
    [SerializeField] private TextMeshProUGUI _playerTwoWinsText;
    [SerializeField] private TextMeshProUGUI _drawsText;
    [SerializeField] private TextMeshProUGUI _avgDurationText;


    private void OnEnable()
    {
        RefreshStats();
    }
    public void RefreshStats()
    {
        var s = StatsManager.instance;

        if (s == null)
        {
            Debug.LogError("StatsManager instance is null!");
            return;
        }

        if (_totalGamesText == null) { Debug.LogError("_totalGamesText not assigned!"); return; }
        if (_playerOneWinsText == null) { Debug.LogError("_playerOneWinsText not assigned!"); return; }
        if (_playerTwoWinsText == null) { Debug.LogError("_playerTwoWinsText not assigned!"); return; }
        if (_drawsText == null) { Debug.LogError("_drawsText not assigned!"); return; }
        if (_avgDurationText == null) { Debug.LogError("_avgDurationText not assigned!"); return; }


        _totalGamesText.text = $"Total Games: {s.GetTotalGames()}";
        _playerOneWinsText.text = $"Player 1 Wins: {s.GetPlayerOneWins()}";
        _playerTwoWinsText.text = $"Player 2 Wins: {s.GetPlayerTwoWins()}";
        _drawsText.text = $"Draws: {s.GetDraws()}";

        float avg = s.GetAverageDuration();
        int m = Mathf.FloorToInt(avg / 60f);
        int sec = Mathf.FloorToInt(avg % 60f);
        _avgDurationText.text = $"Avg Duration: {m}:{sec:00}";
    }
}
