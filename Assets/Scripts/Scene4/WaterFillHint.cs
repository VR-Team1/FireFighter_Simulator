using UnityEngine;

public class WaterFillHint : MonoBehaviour
{
    [Header("안내 문구 UI")]
    public GameObject hintUI;

    [Header("트리거에 반응할 태그 이름")]
    public string targetTag = "Player";

    private void Start()
    {
        if (hintUI != null)
            hintUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (hintUI != null)
                hintUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (hintUI != null)
                hintUI.SetActive(false);
        }
    }
}
