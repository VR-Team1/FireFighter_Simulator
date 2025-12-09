using UnityEngine;

public class HelicopterViewSwitcher : MonoBehaviour
{
    public GameObject fpHeli;
    public GameObject tpHeli;

    void Start()
    {
        bool isFirstPerson;

        if (GameManager.Instance != null)
        {
            isFirstPerson = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        }
        else
        {
            isFirstPerson = false; 
        }

        ApplyView(isFirstPerson);
    }

    private void ApplyView(bool fp)
    {
        if (fpHeli != null) fpHeli.SetActive(fp);
        if (tpHeli != null) tpHeli.SetActive(!fp);

        Debug.Log($"[HelicopterViewSwitcher] FP Mode = {fp}");
    }
}
