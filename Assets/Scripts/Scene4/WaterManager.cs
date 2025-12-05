using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public Transform heli;            // 헬기 Transform
    public float heightLimit = 150f;    // 물 위 20m까지 허용
    MeshCollider meshColl;

    float waterPercent = 0f;

    float halfWidth;
    float halfDepth;

    void Start()
    {
        meshColl = this.GetComponent<MeshCollider>();
    }

    void Update()
    {
        if ( heli == null)
        {
            heli = GameObject.Find("ModelHolder").transform;
        }
        CheckWaterGet();
    }

    void CheckWaterGet()
    {
        Vector3 hPos = heli.position;
        Vector3 pPos = transform.position;

        // Plane 실제 폭 (Plane 기본 크기 10 × scale)
        float halfWidth = 5f * transform.localScale.x;
        float halfDepth = 5f * transform.localScale.z;

        // XZ 평면 안에 있는지 직접 계산
        bool insidePlane =
            Mathf.Abs(hPos.x - pPos.x) <= halfWidth &&
            Mathf.Abs(hPos.z - pPos.z) <= halfDepth;

        if (!insidePlane)
        {
            return;
        }

        // 2) 물 위 높이
        if (Physics.Raycast(hPos, Vector3.down, out RaycastHit hit, 500f))
        {
            // 물 Plane 맞으면
            if (hit.collider == meshColl)
            {
                float realHeight = hit.distance;  // ← ★ 헬기 바닥과 물 사이 진짜 거리!

                Debug.Log("헬기 바닥~물 거리: " + realHeight);

                // 높이가 제한 이내일 때만 물 채움
                if (realHeight <= heightLimit)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        waterPercent = Mathf.Min(100f, waterPercent + 0.5f);
                        Debug.Log("물 획득: " + waterPercent);
                    }
                }
            }
        }
    }
}
