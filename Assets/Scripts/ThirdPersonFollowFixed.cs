using UnityEngine;

public class ThirdPersonFollowFixed : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 25f, -50f);
    public float followSpeed = 5f;

    public Vector3 fixedEulerAngles = new Vector3(25f, 0f, 0f);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(fixedEulerAngles);
    }
}
