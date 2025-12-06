using UnityEngine;

public class Scene4Start : MonoBehaviour
{
    [Header("처음에 띄울 안내 패널")]
    public GameObject startHintPanel;
    private bool started = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startHintPanel != null)
            startHintPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (started) return;
        if (Input.anyKeyDown)
        {
            started = true;

            if (startHintPanel != null)
                startHintPanel.SetActive(false);
        }
    }
}