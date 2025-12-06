using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public Transform heli;             // 헬리콥터 Transform
    public float heightLimit = 150f;   // 물을 퍼올릴 수 있는 최대 높이
    public float waterPercent = 0f;    // 0 ~ 100%
    public bool isInsidePlane = false;

    private Transform[] waterPlanes;

    void Start()
    {
        // 자식 Plane들을 전부 가져옴
        waterPlanes = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waterPlanes[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        if (heli == null)
        {
            heli = GameObject.Find("ModelHolder").transform;
        }

        CheckWaterGet();
    }

    void CheckWaterGet()
    {
        foreach (Transform plane in waterPlanes)
        {
            Vector3 pPos = plane.position;
            Vector3 hPos = heli.position;

            // Plane 스케일 기반 실제 반지름
            float halfWidth = 5f * plane.localScale.x;
            float halfDepth = 5f * plane.localScale.z;

            // XZ 범위 판정
            bool insidePlane =
                Mathf.Abs(hPos.x - pPos.x) <= halfWidth &&
                Mathf.Abs(hPos.z - pPos.z) <= halfDepth;

            if (insidePlane)
            {
                isInsidePlane = true;
                float height = hPos.y - pPos.y;

                if (height <= heightLimit && Input.GetKey(KeyCode.Space))
                {
                    waterPercent = Mathf.Min(100f, waterPercent + 0.5f);
                }

                return; // 이미 한 Plane 위에 있으므로 다른 Plane 검사 필요 없음
            }
        }

        isInsidePlane = false;
        // 물 위에 없는데 Space 누르면 감소
        if (Input.GetKey(KeyCode.Space))
        {
            waterPercent = Mathf.Max(0f, waterPercent - 0.1f);
        }
    }
    public float GetWaterPercent()
    {
        return waterPercent;
    }
}
