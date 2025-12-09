using UnityEngine;

public class HeliWaterDrop : MonoBehaviour
{
    [Header("물 파티클")]
    public ParticleSystem waterParticle;

    [Header("물 관리 매니저")]
    public WaterManager waterManager;

    [Header("물 떨어질 때 나는 소리")]
    public AudioSource waterAudio;

    private bool isDropping = false;

    void Start()
    {

        if (waterParticle != null)
        {
            var emission = waterParticle.emission;
            emission.enabled = false;
        }

        if (waterAudio != null)
        {
            waterAudio.loop = true; 
            waterAudio.Stop();
        }
    }

    void Update()
    {
        bool hasWater = waterManager != null && waterManager.GetWaterPercent() > 0f;
        bool isOutsideWater = waterManager != null && waterManager.isInsidePlane == false;

        bool shouldDrop = Input.GetKey(KeyCode.E) && hasWater && isOutsideWater;

        if (shouldDrop && !isDropping)
        {
            StartDrop();
        }
        else if (!shouldDrop && isDropping)
        {
            StopDrop();
        }
    }

    void StartDrop()
    {
        isDropping = true;

        if (waterParticle != null)
        {
            var emission = waterParticle.emission;
            emission.enabled = true;
        }
        if (waterAudio != null && !waterAudio.isPlaying)
        {
            waterAudio.Play();
        }
    }

    void StopDrop()
    {
        isDropping = false;

        if (waterParticle != null)
        {
            var emission = waterParticle.emission;
            emission.enabled = false;
        }

        if (waterAudio != null && waterAudio.isPlaying)
        {
            waterAudio.Stop();
        }
    }
}
