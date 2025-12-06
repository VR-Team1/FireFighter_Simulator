using UnityEngine;

public class HeliWaterDrop : MonoBehaviour
{
    public ParticleSystem waterParticle;

    void Start()
    {
        var emission = waterParticle.emission;
        emission.enabled = false; // Ã³À½¿¡´Â ²¨µÒ
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // ½ºÆäÀÌ½º·Î ¹° »Ñ¸®±â
        {
            var emission = waterParticle.emission;
            emission.enabled = true;
        }
        else
        {
            var emission = waterParticle.emission;
            emission.enabled = false;
        }
    }
}
