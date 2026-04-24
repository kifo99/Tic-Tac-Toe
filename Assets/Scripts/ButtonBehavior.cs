using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{


    public void RetryGame()
    {
        GameManager.instance.IncreaseGameCounter();
        GameManager.instance.StartNewGame();
    }

    public void Exit()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
