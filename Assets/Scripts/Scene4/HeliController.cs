using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HeliController : MonoBehaviour
{
    Rigidbody rb;
    PlayerControls input;

    float pitchInput;
    float yawInput;
    float liftInput;
    float rollInput;

    float currentPitch = 0f;
    float currentRoll = 0f;

    [Header("Pitch Settings")]
    public float pitchSpeed = 40f;
    public float pitchReturnSpeed = 6f;
    public float maxPitchDown = -45f;
    public float maxPitchUp = 20f;

    [Header("Roll Settings")]
    public float rollSpeed = 50f;
    public float rollReturnSpeed = 6f;
    public float maxRoll = 25f;   // 유진 요청값

    [Header("Movement Settings")]
    public float yawSpeed = 70f;     // 회전 (좌우)
    public float liftPower = 20f;    // 상승/하강 힘
    public float forwardPower = 30f; // 전진력
    public float sidePower = 25f;    // ← 횡이동력 (46의 핵심)

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerControls();

        rb.linearDamping = 0.5f;          // 선형 감속
        rb.angularDamping = 1.5f;   // 회전 감속
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        Vector2 tilt = input.Helicopter.Tilt.ReadValue<Vector2>();

        pitchInput = tilt.y;               // NUM8/2 → pitch
        rollInput = tilt.x;               // NUM4/6 → roll

        yawInput = input.Helicopter.Yaw.ReadValue<float>();
        liftInput = input.Helicopter.Lift.ReadValue<float>();
    }

    void FixedUpdate()
    {
        UpdatePitch();
        UpdateRoll();
        ApplyRotation();

        ApplyLift();
        ApplyForwardMotion();
        ApplyStrafe();      // ★ 횡이동 추가됨
    }

    // ===========================
    //       Pitch 처리
    // ===========================
    void UpdatePitch()
    {
        if (pitchInput != 0)
            currentPitch += pitchInput * pitchSpeed * Time.fixedDeltaTime;
        else
            currentPitch = Mathf.Lerp(currentPitch, 0f, pitchReturnSpeed * Time.fixedDeltaTime);

        currentPitch = Mathf.Clamp(currentPitch, maxPitchDown, maxPitchUp);
    }

    // ===========================
    //       Roll 처리
    // ===========================
    void UpdateRoll()
    {
        if (rollInput != 0)
            currentRoll += rollInput * rollSpeed * Time.fixedDeltaTime;
        else
            currentRoll = Mathf.Lerp(currentRoll, 0f, rollReturnSpeed * Time.fixedDeltaTime);

        currentRoll = Mathf.Clamp(currentRoll, -maxRoll, maxRoll);
    }

    // ===========================
    //   Pitch + Roll + Yaw 적용
    // ===========================
    void ApplyRotation()
    {
        // pitch + roll 적용
        Quaternion baseRot = Quaternion.Euler(currentPitch, transform.localEulerAngles.y, currentRoll);
        transform.localRotation = baseRot;

        // yaw 회전 (오직 입력만)
        float yawRot = yawInput * yawSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, yawRot, 0f, Space.Self);
    }

    // ===========================
    //      Lift (상승/하강)
    // ===========================
    void ApplyLift()
    {
        rb.AddForce(Vector3.up * (liftInput * liftPower), ForceMode.Acceleration);
    }

    // ===========================
    //    Forward Motion (Pitch)
    // ===========================
    void ApplyForwardMotion()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        float forwardFactor = pitchInput;

        rb.AddForce(forward * (forwardFactor * forwardPower), ForceMode.Acceleration);
    }

    // ===========================
    //     Strafe Motion (Roll)
    // ===========================
    void ApplyStrafe()
    {
        float strafeFactor = currentRoll / maxRoll;  // -1 ~ 1

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        // Roll 기울기 → 횡이동
        rb.AddForce(right * (strafeFactor * sidePower), ForceMode.Acceleration);
    }
}
