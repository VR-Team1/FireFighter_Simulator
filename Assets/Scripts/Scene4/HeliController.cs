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

    float currentPitch = 0f;

    [Header("Pitch Settings")]
    public float pitchSpeed = 40f;
    public float pitchReturnSpeed = 3f;
    public float maxPitchDown = -45f;
    public float maxPitchUp = 20f;

    [Header("Movement Settings")]
    public float yawSpeed = 70f;
    public float liftPower = 20f;
    public float forwardPower = 30f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerControls();
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        Vector2 tilt = input.Helicopter.Tilt.ReadValue<Vector2>();
        pitchInput = tilt.y;              // NUM8 = +1 (아래로 기수)
        yawInput = input.Helicopter.Yaw.ReadValue<float>();
        liftInput = input.Helicopter.Lift.ReadValue<float>();
    }

    void FixedUpdate()
    {
        UpdatePitch();
        ApplyYaw();
        ApplyLift();
        ApplyForwardMotion();
    }

    void UpdatePitch()
    {
        if (pitchInput != 0)
        {
            currentPitch += pitchInput * pitchSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentPitch = Mathf.Lerp(currentPitch, 0f, pitchReturnSpeed * Time.fixedDeltaTime);
        }

        currentPitch = Mathf.Clamp(currentPitch, maxPitchDown, maxPitchUp);

        Vector3 e = transform.localEulerAngles;
        e.x = currentPitch;
        transform.localEulerAngles = e;
    }

    void ApplyYaw()
    {
        float yawRot = yawInput * yawSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, yawRot, 0f, Space.Self);
    }

    void ApplyLift()
    {
        rb.AddForce(Vector3.up * (liftInput * liftPower), ForceMode.Acceleration);
    }

    void ApplyForwardMotion()
    {
        // 수평 forward 벡터 (y 성분 제거)
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();
        float forwardFactor = pitchInput;

        rb.AddForce(forward * (forwardFactor * forwardPower), ForceMode.Acceleration);
    }
}
