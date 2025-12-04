using UnityEngine;

public class ViewModeCameraController : MonoBehaviour
{
    public Camera fpCamera;   // 1인칭 카메라
    public Camera tpCamera;   // 3인칭 카메라

    void Start()
    {
        ApplyViewMode();
    }

    private void ApplyViewMode()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager 없음 - 기본으로 3인칭 사용");
            SetMode(false);
            return;
        }

        bool isFirstPerson = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        SetMode(isFirstPerson);
    }

    private void SetMode(bool firstPerson)
    {
        if (fpCamera == null || tpCamera == null) return;

        fpCamera.gameObject.SetActive(firstPerson);
        tpCamera.gameObject.SetActive(!firstPerson);

        if (firstPerson)
        {
            fpCamera.tag = "MainCamera";
            tpCamera.tag = "Untagged";
        }
        else
        {
            tpCamera.tag = "MainCamera";
            fpCamera.tag = "Untagged";
        }
    }
}
