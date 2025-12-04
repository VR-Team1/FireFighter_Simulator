using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private Button firstPersonButton;
    [SerializeField] private Button thirdPersonButton;

    private void Start()
    {
        firstPersonButton.onClick.AddListener(OnClickFirstPerson);
        thirdPersonButton.onClick.AddListener(OnClickThirdPerson);
    }

    private void OnClickFirstPerson()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CurrentViewMode = ViewMode.FirstPerson;
        }

        SceneManager.LoadScene("3_GameScene1");
    }

    private void OnClickThirdPerson()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CurrentViewMode = ViewMode.ThirdPerson;
        }

        SceneManager.LoadScene("3_GameScene1");
    }
}
