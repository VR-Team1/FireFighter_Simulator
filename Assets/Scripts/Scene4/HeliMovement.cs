using UnityEngine;
using UnityEngine.InputSystem;

public class HeliMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    Vector2 movement;

    void Update()
    {
        // A/D = left/right → 헬기 기준 좌우 이동
        Vector3 rightMove = transform.right * movement.x;

        // W/S = forward/back → 헬기 기준 앞뒤 이동
        Vector3 forwardMove = transform.forward * movement.y;

        // 최종 이동 벡터
        Vector3 move = (rightMove + forwardMove) * controlSpeed * Time.deltaTime;

        transform.position += move;
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}
