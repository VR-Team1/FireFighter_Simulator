using UnityEngine;
using UnityEngine.UI;  

public class FireFightManager : MonoBehaviour
{
    [SerializeField] public float targetHit = 3.0f; 
    private float hit = 0f;

    public float firePercent { get; private set; } = 100f;

    [Header("왼쪽 아래 불 게이지 Fill 이미지")]
    public Image fireGaugeFill;

    void Start()
    {
        if (fireGaugeFill != null)
        {
            fireGaugeFill.fillAmount = 1f;  
        }
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
        {
            fireGaugeFill.fillAmount = firePercent / 100f;
        }

        Debug.Log($"[Fire] hit: {hit}, firePercent: {firePercent}, fill={fireGaugeFill?.fillAmount}");

        if (hit >= targetHit)
        {
            firePercent = 0f;

            if (fireGaugeFill != null)
                fireGaugeFill.fillAmount = 0f;

            Destroy(gameObject);
        }
    }
}
