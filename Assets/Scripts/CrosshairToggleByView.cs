using UnityEngine;

public class CrosshairToggleByView : MonoBehaviour
{
    void Update()
    {
        bool isFirstPerson = false;

        if (GameManager.Instance != null)
        {
            isFirstPerson = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        }

        gameObject.SetActive(isFirstPerson);
    }
}
