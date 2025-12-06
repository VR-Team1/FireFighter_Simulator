using UnityEngine;

public class HelicopterViewSwitcher : MonoBehaviour
{
    public GameObject fpHeli;
    public GameObject tpHeli;

    void Start()
    {
        bool isFirstPerson = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        ApplyView(isFirstPerson);
    }

    private void ApplyView(bool fp)
    {
        if (fpHeli != null) fpHeli.SetActive(fp);
        if (tpHeli != null) tpHeli.SetActive(!fp);

        Debug.Log($"[HelicopterViewSwitcher] FP Mode = {fp}");
    }
}
