using UnityEngine;
using UnityEngine.UI;

public class FireFightManager : MonoBehaviour
{
    [SerializeField] public float targetHit = 3f;
    private float hit = 0f;

    public float firePercent { get; private set; } = 100f;

    [Header("왼쪽 아래 불 게이지 Fill 이미지")]
    public Image fireGaugeFill;

    [Header("불 오브젝트(크기 줄이고 싶은 오브젝트)")]
    public Transform fireVisual;   
    private Vector3 initialScale;

    void Start()
    {
        if (fireGaugeFill != null)
            fireGaugeFill.fillAmount = 1f;

        if (fireVisual == null)
            fireVisual = transform; 

        initialScale = fireVisual.localScale; 
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Water"))
            return;

        hit++;

        float ratio = hit / targetHit;
        firePercent = 100f - (ratio * 100f);
        firePercent = Mathf.Clamp(firePercent, 0f, 100f);

        if (fireGaugeFill != null)
            fireGaugeFill.fillAmount = firePercent / 100f;

        float scaleRatio = Mathf.Clamp01(1f - ratio);
        fireVisual.localScale = initialScale * scaleRatio;

        Debug.Log($"[Fire] hit: {hit}, firePercent: {firePercent}, scale={fireVisual.localScale}");

        if (hit >= targetHit)
        {
            firePercent = 0f;

            if (fireGaugeFill != null)
                fireGaugeFill.fillAmount = 0f;

            Destroy(gameObject);
        }
    }
}
