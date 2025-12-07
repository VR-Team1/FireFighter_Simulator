using UnityEngine;

public class Scene4Start : MonoBehaviour
{
    [Header("처음에 띄울 안내 패널")]
    public GameObject startHintPanel;

    [Header("타이머 UI")]
    public TimeUI timeUI;

    private bool started = false;

    void Start()
    {
        if (startHintPanel != null)
            startHintPanel.SetActive(true);

        if (timeUI != null)
            timeUI.isRunning = false;
    }

    void Update()
    {
        if (started) return;

        if (Input.anyKeyDown)
        {
            started = true;

            if (startHintPanel != null)
                startHintPanel.SetActive(false);

            if (timeUI != null)
                timeUI.StartTimer();
        }
    }
}
