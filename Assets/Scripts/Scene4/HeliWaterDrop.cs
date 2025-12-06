using UnityEngine;

public class HeliWaterDrop : MonoBehaviour
{
    public ParticleSystem waterParticle;
    public WaterManager waterManager;

    void Start()
    {
        var emission = waterParticle.emission;
        emission.enabled = false;
    }

    void Update()
    {
        bool hasWater = waterManager.GetWaterPercent() > 0f;
        bool isOutsideWater = waterManager.isInsidePlane == false;

        if (Input.GetKey(KeyCode.E) && hasWater && isOutsideWater)
        {
            var emission = waterParticle.emission;
            emission.enabled = true;

            // 원하면 물 감소도 추가 가능
            // waterManager.UseWater(Time.deltaTime * 5f);
        }
        else
        {
            var emission = waterParticle.emission;
            emission.enabled = false;
        }
    }
}
