using UnityEngine;

public class WaterUIController : MonoBehaviour
{
    public GameObject fpWaterUI;
    public GameObject tpWaterUI;

    void Start()
    {
        bool isFP = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        ApplyView(isFP);
    }

    private void ApplyView(bool fp)
    {
        if (fpWaterUI != null) fpWaterUI.SetActive(fp);
        if (tpWaterUI != null) tpWaterUI.SetActive(!fp);

        Debug.Log($"[WaterUIController] FP Mode = {fp}");
    }
}
