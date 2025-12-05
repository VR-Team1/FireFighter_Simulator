using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 20f;
    public float runSpeed = 160f;
    public float rotationSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        bool isFirstPerson =
            GameManager.Instance != null &&
            GameManager.Instance.CurrentViewMode == ViewMode.FirstPerson;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0f, v);
        bool isMoving = inputDir.sqrMagnitude > 0.01f;

        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 moveDir = Vector3.zero;
        if (isMoving && Camera.main != null)
        {
            Transform cam = Camera.main.transform;

            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(cam.right, new Vector3(1, 0, 1)).normalized;

            moveDir = (camForward * v + camRight * h).normalized;

            if (!isFirstPerson)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        controller.Move(moveDir * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("MoveSpeed", isMoving ? currentSpeed : 0f);
            animator.SetBool("IsRunning", isRunning);

            animator.speed = isMoving ? 1f : 0f;
        }
    }
}
