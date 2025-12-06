using UnityEngine;

public class SceneStartHintController : MonoBehaviour
{
    [Header("처음에 띄울 안내 패널")]
    public GameObject startHintPanel;

    [Header("플레이어 이동 스크립트")]
    public PlayerMovement playerMovement;

    [Header("십자선 UI")]
    public GameObject crosshair;

    [Header("오디오 소스")]
    public AudioSource announceAudio; 

    [Header("사이렌 루프 컨트롤러")]
    public SirenLooper sirenLooper;

    [Header("빨간 화면 깜빡임 컨트롤러")]
    public RedScreenFlash redScreenFlash;

    private bool started = false;

    private void Start()
    {
        if (startHintPanel != null)
            startHintPanel.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (crosshair != null)
            crosshair.SetActive(false);
    }

    private void Update()
    {
        if (started) return;

        if (Input.anyKeyDown)
        {
            started = true;

            if (startHintPanel != null)
                startHintPanel.SetActive(false);

            if (playerMovement != null)
                playerMovement.enabled = true;

            if (crosshair != null)
                crosshair.SetActive(true);

            if (announceAudio != null)
                StartCoroutine(PlayAnnounceThenSiren());
        }
    }

    private System.Collections.IEnumerator PlayAnnounceThenSiren()
    {
        announceAudio.Play();

        yield return new WaitForSeconds(announceAudio.clip.length);

        if (sirenLooper != null)
            sirenLooper.PlaySirenLoop();

        if (redScreenFlash != null)
            redScreenFlash.StartFlash();
    }
}
