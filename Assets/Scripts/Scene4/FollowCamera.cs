using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 worldOffset = new Vector3(0, 8, -20);
    public float smooth = 5f;

    // 고정 시야각
    public float fixedPitch = 20f; // 위쪽에서 내려다보기
    public float fixedYawOffset = 0f;

    void LateUpdate()
    {
        if (!target) return;

        // 1) 카메라 위치 고정
        Vector3 desiredPos = target.position + target.transform.rotation * worldOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        // 2) 카메라는 헬기의 yaw만 따라감
        float yaw = target.eulerAngles.y + fixedYawOffset;

        // 3) pitch는 고정
        float pitch = fixedPitch;

        // 4) roll은 항상 0
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
