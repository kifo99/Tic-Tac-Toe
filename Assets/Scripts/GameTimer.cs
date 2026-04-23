using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    private float _timeElapsed = 0f;
    private bool _isRunning = false;

    public void StartTimer()
    {
        _timeElapsed = 0f;
        _isRunning = true;
    }


    public void StopTimer()
    {
        _isRunning = false;

    }
    public void UpdateTimerUi()
    {
        if (_isRunning)
        {
            _timeElapsed += Time.deltaTime;

            int minutes = Mathf.FloorToInt(_timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(_timeElapsed % 60f);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
