using UnityEngine;

public class WaterUIController : MonoBehaviour
{
    public GameObject fpWaterUI;
    public GameObject tpWaterUI;

    void Start()
    {
        bool isFP;

        if (GameManager.Instance != null)
        {
            isFP = GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;
        }
        else
        {
            isFP = false;
        }

        ApplyView(isFP);
    }

    private void ApplyView(bool fp)
    {
        if (fpWaterUI != null) fpWaterUI.SetActive(fp);
        if (tpWaterUI != null) tpWaterUI.SetActive(!fp);
    }
}
