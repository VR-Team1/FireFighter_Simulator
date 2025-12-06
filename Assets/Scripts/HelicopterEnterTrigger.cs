using UnityEngine;
using UnityEngine.SceneManagement;

public class HelicopterEnterTrigger : MonoBehaviour
{
    [Header("다음에 로드할 씬 이름")]
    public string nextSceneName = "4_GameScene2 1";

    [Header("E 키 안내 UI")]
    public GameObject enterPromptUI;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (enterPromptUI != null)
                enterPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (enterPromptUI != null)
                enterPromptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("다음 씬 이름이 설정되지 않았습니다.");
            }
        }
    }
}
