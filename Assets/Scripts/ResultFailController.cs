using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultFailController : MonoBehaviour
{
    [Header("재시도할 게임 씬 이름")]
    [SerializeField] private string gameSceneName = "4_GameScene2 1";

    public void OnClickRetry()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
