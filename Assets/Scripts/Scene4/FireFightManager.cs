using UnityEngine;

public class FireFightManager : MonoBehaviour
{
    [SerializeField] public float targetHit = 3.0f;
    float hit = 0f;

    public float firePercent { get; private set; } = 100f;

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            hit++;

            float ratio = hit / targetHit;
            firePercent = 100f - (ratio * 100f);
            firePercent = Mathf.Clamp(firePercent, 0f, 100f);

            if (hit >= targetHit)
            {
                firePercent = 0f;
                Destroy(this.gameObject);
            }
        }
    }
}
