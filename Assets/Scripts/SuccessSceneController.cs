using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessSceneController : MonoBehaviour
{
    public void OnClickGoToTitle()
    {
        SceneManager.LoadScene("2_TitleScene");
    }
}
