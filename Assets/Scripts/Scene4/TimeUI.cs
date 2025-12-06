using UnityEngine;
using TMPro;
public class TimeUI : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeLeft = 60f;
    public float count = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft <= 0f)
        {
            timeLeft = 0f;
            return;
        }

        // 1초 누적 시간 계산
        count -= Time.deltaTime;
        // 1초가 지났을 때 timeLeft 감소
        if (count <= 0f)
        {
            timeLeft -= 1f; // ★ 감소
            count = 1f;     // 다시 1초 세팅
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        int seconds = Mathf.CeilToInt(timeLeft);
        timerText.text = $"0:{seconds:00}";
    }
}
