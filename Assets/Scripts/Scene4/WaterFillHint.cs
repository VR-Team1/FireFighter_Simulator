using UnityEngine;

public class WaterFillHint : MonoBehaviour
{
    [Header("안내 문구 UI")]
    public GameObject hintUI;

    [Header("트리거에 반응할 태그 이름")]
    public string targetTag = "Player";

    [Header("물 채우기 관련")]
    public WaterManager waterManager;

    private void Start()
    {
        if (hintUI != null)
            hintUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        if (hintUI != null)
            hintUI.SetActive(true);

        if (waterManager != null)
            waterManager.isInBottomZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        if (hintUI != null)
            hintUI.SetActive(false);

        if (waterManager != null)
            waterManager.isInBottomZone = false; 
    }
}
