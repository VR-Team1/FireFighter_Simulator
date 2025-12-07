using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeLeft = 60f;
    public float count = 1f;

    public bool isRunning = false;

    void Update()
    {
        if (!isRunning) return;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            return;
        }

        count -= Time.deltaTime;

        if (count <= 0f)
        {
            timeLeft -= 1f; 
            count = 1f; 
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        int seconds = Mathf.CeilToInt(timeLeft);
        timerText.text = $"0:{seconds:00}";
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
