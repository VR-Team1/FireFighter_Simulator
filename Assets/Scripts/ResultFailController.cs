using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultFailController : MonoBehaviour
{
    [Header("재시도할 게임 씬 이름")]
    [SerializeField] private string gameSceneName = "4_GameScene2 1";

    private void Start()
    {
        // 실패 화면에서는 마우스 보이게 + 잠금 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;   // 혹시 정지시켜놨으면 풀어주기
    }

    public void OnClickRetry()
    {
        // 다시 게임으로 들어가니까 마우스 잠그고 숨기기 (1인칭용)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(gameSceneName);
    }
}
