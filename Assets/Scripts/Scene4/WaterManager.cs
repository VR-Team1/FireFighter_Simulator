using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public Transform heli; 
    public float heightLimit = 150f;  
    public float waterPercent = 0f;   

    public bool isInsidePlane = false;
    public bool isInBottomZone = false; 

    private Transform plane;

    void Start()
    {
        plane = transform;
    }

    void Update()
    {
        if (heli == null)
        {
            var heliObj = GameObject.Find("Helicopter");
            if (heliObj == null) return;
            heli = heliObj.transform;
        }

        CheckWaterGet();
    }

    void CheckWaterGet()
    {
        Vector3 pPos = plane.position;
        Vector3 hPos = heli.position;

        float halfWidth = plane.localScale.x * 5f;
        float halfDepth = plane.localScale.z * 5f;

        bool inside =
            Mathf.Abs(hPos.x - pPos.x) <= halfWidth &&
            Mathf.Abs(hPos.z - pPos.z) <= halfDepth;

        isInsidePlane = inside;

        if (!inside)
        {
            if (Input.GetKey(KeyCode.E))
                waterPercent = Mathf.Max(0f, waterPercent - 0.05f);
            return;
        }

        float height = hPos.y - pPos.y;

        if (height <= heightLimit && isInBottomZone && Input.GetKey(KeyCode.E))
        {
            waterPercent = Mathf.Min(100f, waterPercent + 0.5f);
        }
    }

    public float GetWaterPercent()
    {
        return waterPercent;
    }
}
