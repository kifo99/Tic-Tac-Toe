using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    private const string TOTAL_GAMES_KEY = "TotalGames";
    private const string P1_WINS_KEY = "PlayerOneWins";
    private const string P2_WINS_KEY = "PlayerTwoWins";
    private const string DRAWS_KEY = "Draws";
    private const string TOTAL_DURATION_KEY = "TotalDuration";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void RecordGame(GameResults results, float duration)
    {
        PlayerPrefs.SetInt(TOTAL_GAMES_KEY, GetTotalGames() + 1);
        PlayerPrefs.SetFloat(TOTAL_DURATION_KEY, GetTotalDuration() + duration);

        if (results == GameResults.playerOneWin)
        {
            PlayerPrefs.SetInt(P1_WINS_KEY, GetPlayerOneWins() + 1);
        }
        else if (results == GameResults.playerTwoWin)
        {
            PlayerPrefs.SetInt(P2_WINS_KEY, GetPlayerTwoWins() + 1);
        }
        else if (results == GameResults.draw)
        {
            PlayerPrefs.SetInt(DRAWS_KEY, GetDraws() + 1);
        }

        PlayerPrefs.Save();
    }

    public int GetTotalGames() => PlayerPrefs.GetInt(TOTAL_GAMES_KEY, 0);
    public int GetPlayerOneWins() => PlayerPrefs.GetInt(P1_WINS_KEY, 0);
    public int GetPlayerTwoWins() => PlayerPrefs.GetInt(P2_WINS_KEY, 0);
    public int GetDraws() => PlayerPrefs.GetInt(DRAWS_KEY, 0);
    public float GetTotalDuration() => PlayerPrefs.GetFloat(TOTAL_DURATION_KEY, 0f);

    public float GetAverageDuration()
    {
        int total = GetTotalGames();
        return total > 0 ? GetTotalDuration() / total : 0f;
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey(TOTAL_GAMES_KEY);
        PlayerPrefs.DeleteKey(P1_WINS_KEY);
        PlayerPrefs.DeleteKey(P2_WINS_KEY);
        PlayerPrefs.DeleteKey(DRAWS_KEY);
        PlayerPrefs.DeleteKey(TOTAL_DURATION_KEY);
        PlayerPrefs.Save();
    }
}
