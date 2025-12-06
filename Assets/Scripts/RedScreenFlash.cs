using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RedScreenFlash : MonoBehaviour
{
    [Header("빨간 오버레이 이미지")]
    public Image redOverlay;

    [Header("최대 알파값 (0 ~ 1)")]
    public float maxAlpha = 0.5f;

    [Header("깜빡이는 속도")]
    public float flashSpeed = 2f;

    private bool isFlashing = false;
    private Coroutine flashCo;

    private void Awake()
    {
        if (redOverlay == null)
            redOverlay = GetComponent<Image>();

        if (redOverlay == null)
        {
            Debug.LogError("[RedScreenFlash] Image 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        var c = redOverlay.color;
        c.a = 0f;
        redOverlay.color = c;
    }

    public void StartFlash()
    {
        if (redOverlay == null)
        {
            Debug.LogWarning("[RedScreenFlash] redOverlay가 비어 있어요.");
            return;
        }

        Debug.Log("[RedScreenFlash] StartFlash 호출됨!");

        if (flashCo != null)
            StopCoroutine(flashCo);

        isFlashing = true;
        flashCo = StartCoroutine(FlashRoutine());
    }

    public void StopFlash()
    {
        isFlashing = false;

        if (flashCo != null)
            StopCoroutine(flashCo);

        if (redOverlay != null)
        {
            var c = redOverlay.color;
            c.a = 0f;
            redOverlay.color = c;
        }
    }

    private IEnumerator FlashRoutine()
    {
        float t = 0f;

        while (isFlashing)
        {
            t += Time.deltaTime * flashSpeed;

            float alpha = (Mathf.Sin(t) * 0.5f + 0.5f) * maxAlpha;

            var c = redOverlay.color;
            c.a = alpha;
            redOverlay.color = c;

            yield return null;
        }
    }
}
