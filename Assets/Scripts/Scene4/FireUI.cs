using UnityEngine;
using UnityEngine.UI;

public class FireUI : MonoBehaviour
{
    public Image gaugeImg;
    public FireFightManager fireManager;

    void Update()
    {
        if (gaugeImg == null || fireManager == null) return;

        gaugeImg.fillAmount = fireManager.firePercent / 100f;
    }
}
