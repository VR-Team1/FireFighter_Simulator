using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public Transform heli;             // 헬리콥터 Transform
    public float heightLimit = 150f;   // 물을 퍼올릴 수 있는 최대 높이
    public float waterPercent = 0f;    // 물 게이지 (0~100)
    public bool isInsidePlane = false;

    // 내부 캐시
    private Transform plane;           // 자기 자신

    void Start()
    {
        // 자식이 없다는 전제 → 자기 자신이 Plane이다
        plane = transform;
    }

    void Update()
    {
        if (heli == null)
        {
            heli = GameObject.Find("Helicopter").transform;
            if (heli == null) return;
        }

        CheckWaterGet();
    }

    void CheckWaterGet()
    {
        Vector3 pPos = plane.position;
        Vector3 hPos = heli.position;

        // Unity Plane: 1 scale = 실제 10m
        float halfWidth = plane.localScale.x * 5f;
        float halfDepth = plane.localScale.z * 5f;

        // XZ 범위 체크 (Plane 중심 기준)
        bool inside =
            Mathf.Abs(hPos.x - pPos.x) <= halfWidth &&
            Mathf.Abs(hPos.z - pPos.z) <= halfDepth;

        isInsidePlane = inside;

        // 물 위가 아니면 감소
        if (!inside)
        {
            if (Input.GetKey(KeyCode.E))
                waterPercent = Mathf.Max(0f, waterPercent - 0.05f);

            return;
        }

        // 물 위에 있을 때만 높이 판정
        float height = hPos.y - pPos.y;

        if (height <= heightLimit && Input.GetKey(KeyCode.E))
        {
            waterPercent = Mathf.Min(100f, waterPercent + 0.5f);
        }
    }

    public float GetWaterPercent()
    {
        return waterPercent;
    }
}
