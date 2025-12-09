using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessSceneController : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnClick()
    {
        SceneManager.LoadScene("2_TitleScene");
    }
}
