using UnityEngine;

public class ViewModeCameraController : MonoBehaviour
{
    public Camera fpCamera;
    public Camera tpCamera;

    void Start()
    {
        bool isFirstPerson = false;

        if (GameManager.Instance != null)
        {
            isFirstPerson = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        }

        SetMode(isFirstPerson);
    }

    private void SetMode(bool firstPerson)
    {
        if (fpCamera == null || tpCamera == null)
        {
            Debug.LogWarning("카메라 참조가 비어 있음");
            return;
        }

        fpCamera.enabled = firstPerson;
        tpCamera.enabled = !firstPerson;

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

        var fpListener = fpCamera.GetComponent<AudioListener>();
        var tpListener = tpCamera.GetComponent<AudioListener>();

        if (fpListener != null) fpListener.enabled = firstPerson;
        if (tpListener != null) tpListener.enabled = !firstPerson;

        if (firstPerson)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log($"[ViewModeCameraController] FirstPerson = {firstPerson}");
    }
}
