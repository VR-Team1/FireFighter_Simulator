using UnityEngine;

public class FireFightManager : MonoBehaviour
{
    [SerializeField] public float targetHit = 3.0f;
    float hit = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collision with: " + other.name);
        if (other.CompareTag("Water"))
        {
            hit++;

            if (hit >= targetHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
