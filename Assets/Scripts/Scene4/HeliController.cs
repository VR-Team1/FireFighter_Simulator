using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HeliController : MonoBehaviour
{
    Rigidbody rb;
    PlayerControls input;

    Vector3 startPos;
    Quaternion startRot;

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
    public float maxRoll = 25f;

    [Header("Movement Settings")]
    public float yawSpeed = 70f;
    public float liftPower = 20f;
    public float forwardPower = 30f;
    public float sidePower = 25f;

    void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        rb = GetComponent<Rigidbody>();
        input = new PlayerControls();

        // PhysX 5 damping
        rb.linearDamping = 0.3f;
        rb.angularDamping = 1.0f;
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        Vector2 tilt = input.Helicopter.Tilt.ReadValue<Vector2>();

        pitchInput = tilt.y;
        rollInput = tilt.x;

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
        ApplyStrafe();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);

        this.transform.position = startPos;
        this.transform.rotation = startRot;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Terrain hit!");

        transform.position = startPos;
        transform.rotation = startRot;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
        Quaternion baseRot = Quaternion.Euler(currentPitch, transform.localEulerAngles.y, currentRoll);
        transform.localRotation = baseRot;

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
        float strafeFactor = currentRoll / maxRoll;

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        rb.AddForce(right * (strafeFactor * sidePower), ForceMode.Acceleration);
    }
}
